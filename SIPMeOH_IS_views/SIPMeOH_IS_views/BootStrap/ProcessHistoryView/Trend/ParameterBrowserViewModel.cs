// ---------------------------------------------------------------------------------------------
// ParameterBrowserViewModel.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 14/09/2016, 14:40
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Windows.Data;
    using RSI.Common.Mvvm;
    using RSI.Common.Mvvm.Extension;
    using RSI.IndissPlus.Solution;
    using RSI.Kernel.Model.Interfaces.Models;

    public class ParameterBrowserViewModel : WindowViewModel
    {
        private ICollectionView _collectionView;

        private string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterBrowserViewModel"/> class.
        /// </summary>
        public ParameterBrowserViewModel()
        {
            this._title = "Open";
        }

        public override bool CanResize => false;

        public override string Id => "ParameterBrowserViewModel";

        /// <summary>
        ///     Gets or sets the name of the parameter.
        /// </summary>
        /// <value>
        ///     The name of the parameter.
        /// </value>
        public string ParameterName
        {
            get
            {
                return this._parameterName;
            }

            set
            {
                if (this.Set(ref this._parameterName, value) && this._collectionView != null)
                {
                    // user release filter 
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this._collectionView.CurrentChanged -= this.OnCurrentChanged;
                        this._filter = null;
                        this._collectionView.Refresh();
                        this._collectionView.MoveCurrentToPosition(-1);
                        this._collectionView.CurrentChanged += this.OnCurrentChanged;
                        return;
                    }

                    // user set a filter
                    this._collectionView.CurrentChanged -= this.OnCurrentChanged;
                    this._filter = value;
                    this._collectionView.Refresh();
                    this._collectionView.MoveCurrentToPosition(-1);
                    this._collectionView.CurrentChanged += this.OnCurrentChanged;
                }
            }
        }

        public ICollectionView ParameterNames
        {
            get
            {
                if (this._collectionView != null)
                {
                    return this._collectionView;
                }

                var parameters = this.Parameters;
                this._collectionView = CollectionViewSource.GetDefaultView(parameters);
                this._collectionView.Filter = this.Filter;
                this._collectionView.CurrentChanged += this.OnCurrentChanged;
                return this._collectionView;
            }
        }

        private string _filter;
        private NameHolder[] _parameters;

        /// <summary>
        /// Filters the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        private bool Filter(object o)
        {
            if (this._filter == null)
            {
                return true;
            }

            var name = ((NameHolder)o).Name;
            return name.IsMatch(this._filter, RegexOptions.IgnoreCase);
        }

        private NameHolder[] Parameters
        {
            get
            {
                if (this._parameters != null)
                {
                    return this._parameters;
                }

                var units = new List<NameHolder>();
                foreach (var unit in ServiceContext.ModelService.Units)
                {
                    var typename = unit.TypeName;

                    if (typename == "DeltaVPID")
                    {
                        var unitName = unit.Name;
                        units.Add(new NameHolder($"{unitName}.PV", unit));
                        units.Add(new NameHolder($"{unitName}.MV", unit));
                        units.Add(new NameHolder($"{unitName}.SP", unit));
                        units.Add(new NameHolder($"{unitName}.OUT", unit));
                        continue;
                    }

                    if (typename == "DeltaVAI" || typename == "DeltaVAO")
                    {
                        var unitName = unit.Name;
                        units.Add(new NameHolder($"{unitName}.PV", unit));
                    }
                }

                // units.Sort();
                this._parameters = units.ToArray();
                if (this._parameters.Length != 0)
                {
                    this._parameterName = this._parameters[0].Name;
                    this.RaisePropertyChanged(() => this.ParameterName);
                }
                return this._parameters;
            }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        protected override bool Commit()
        {
            if (string.IsNullOrWhiteSpace(this._parameterName))
            {
                ServiceContext.MessageDisplayService.ShowError("Add Parameter", "Parameter name is empty...");
                return false;
            }

            if (!this.Parameters.Any(_ => string.Compare(_.Name, this._parameterName, StringComparison.OrdinalIgnoreCase) == 0))
            {
                ServiceContext.MessageDisplayService.ShowError("Add Parameter", $"{this._parameterName} is not a valid parameter name");
                return false;
            }

            return base.Commit();
        }

        protected override void Dispose(bool isDisposing)
        {
        }

        /// <summary>
        ///     Called when [current changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void OnCurrentChanged(object sender, EventArgs e)
        {
            var currentItem = this._collectionView.CurrentItem;
            this._parameterName = currentItem == null ? string.Empty : ((NameHolder)currentItem).Name;
            this.RaisePropertyChanged("ParameterName");
        }

        public struct NameHolder
        {
            public NameHolder(string name, IUnit unit)
            {
                this.Name = name;
                this.Unit = unit;
            }

            public IUnit Unit
            {
                get;
            }

            public string Name
            {
                get;
            }
        }
    }
}