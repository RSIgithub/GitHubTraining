// ---------------------------------------------------------------------------------------------
// TrendConfigViewModel.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 13/09/2016, 12:01
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model;
    using RSI.Common.Mvvm;
    using RSI.IndissPlus.Solution;

    public class TrendConfigViewModel : WindowViewModel
    {
        /// <summary>
        ///     MoveCurrentDown command field declaration
        /// </summary>
        private RelayCommand _moveCurrentDownCommand;

        /// <summary>
        ///     MoveCurrentUp command field declaration
        /// </summary>
        private RelayCommand _moveCurrentUpCommand;

        public TrendDeltaV Trend
        {
            get;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrendConfigViewModel" /> class.
        /// </summary>
        /// <param name="trend">The trend delta v.</param>
        public TrendConfigViewModel(TrendDeltaV trend)
        {
            this.Trend = trend;
            this._title = "Configure Chart";
            var collectionView = CollectionViewSource.GetDefaultView(trend.Pens);
            var editableView = collectionView as IEditableCollectionView;
            this.CollectionView = collectionView;
        }

        public override bool CanResize => false;

        public override string Id => "DeltaVTrendConfig";

        public ICollectionView CollectionView
        {
            get;
        }

        /// <summary>
        ///     Gets the MoveCurrentDown command.
        /// </summary>
        /// <value>
        ///     The MoveCurrentDown command.
        /// </value>
        public ICommand MoveCurrentDownCommand
        {
            get
            {
                return this._moveCurrentDownCommand
                       ?? (this._moveCurrentDownCommand =
                           new RelayCommand(() =>
                               {
                                   var position = this.CollectionView.CurrentPosition;
                                   var source = this.Trend.Pens[position];
                                   this.Trend.Pens.RemoveAt(position);
                                   this.Trend.Pens.Insert(position + 1, source);
                                   this.CollectionView.MoveCurrentTo(source);
                               },
                               () =>
                                   this.CollectionView.CurrentItem != null &&
                                   this.CollectionView.CurrentPosition != this.Trend.Pens.Count - 1));
            }
        }

        /// <summary>
        /// RemovePen command field declaration
        /// </summary>
        private RelayCommand _removePenCommand;

        /// <summary>
        /// Gets the RemovePen command.
        /// </summary>
        /// <value>
        /// The RemovePen command.
        /// </value>
        public ICommand RemovePenCommand
        {
            get
            {
                return this._removePenCommand
                    ?? (this._removePenCommand =
                        new RelayCommand(() => this.Trend.Pens.RemoveAt(this.CollectionView.CurrentPosition), () => this.CollectionView.CurrentItem != null));

            }
        }

        /// <summary>
        /// AddPen command field declaration
        /// </summary>
        private RelayCommand _addPenCommand;

        /// <summary>
        /// Gets the AddPen command.
        /// </summary>
        /// <value>
        /// The AddPen command.
        /// </value>
        public ICommand AddPenCommand
        {
            get
            {
                return this._addPenCommand
                    ?? (this._addPenCommand =
                        new RelayCommand(this.ExecuteAddPenCommand));

            }
        }
        /// <summary>
        /// Executes the Analyse Command.
        /// </summary>
        private void ExecuteAddPenCommand()
        {
            var vm = new ParameterBrowserViewModel();
            ServiceContext.ObjectWindowManager.OpenModalWindow(vm, true);
            if (vm.IsCommitRequired)
            {
                var parameterName = vm.ParameterName;
                if (this.Trend.Pens.Any(
                        _ => string.Compare(_.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    ServiceContext.MessageDisplayService.ShowError("Add Parameter", $"{parameterName} is already available in the trend");
                    return;
                }

                this.Trend.AddPen(vm.ParameterName);
            }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        protected override bool Commit()
        {
            if (string.IsNullOrWhiteSpace(this.Trend.Name))
            {
                ServiceContext2.MessageDisplayService.ShowError("Title problem", "Please, set a value to the Title field");
                return false;
            }

            // If new Trend
            if (this.Trend.IsNew)
            {
                // Save it
                if (!this.Trend.TrySave())
                {
                    return false;
                }
            }

            return base.Commit();
        }

        /// <summary>
        ///     Gets the MoveCurrentUp command.
        /// </summary>
        /// <value>
        ///     The MoveCurrentUp command.
        /// </value>
        public ICommand MoveCurrentUpCommand
        {
            get
            {
                return this._moveCurrentUpCommand
                       ?? (this._moveCurrentUpCommand =
                           new RelayCommand(() =>
                               {
                                   var position = this.CollectionView.CurrentPosition;
                                   var source = this.Trend.Pens[position];
                                   this.Trend.Pens.RemoveAt(position);
                                   this.Trend.Pens.Insert(position - 1, source);
                                   this.CollectionView.MoveCurrentTo(source);
                               },
                               () => this.CollectionView.CurrentItem != null && this.CollectionView.CurrentPosition != 0));
            }
        }

        protected override void Dispose(bool isDisposing)
        {
        }

        /// <summary>
        ///     Determines whether this instance [can execute Analyse Command].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance [can execute Analyse Command]; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecuteMoveCurrentDownCommand()
        {
            return true;
        }

        /// <summary>
        ///     Executes the Analyse Command.
        /// </summary>
        private void ExecuteMoveCurrentDownCommand()
        {
            throw new NotImplementedException();
        }
    }
}