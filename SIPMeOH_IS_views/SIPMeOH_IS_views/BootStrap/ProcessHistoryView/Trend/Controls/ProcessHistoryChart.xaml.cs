// ---------------------------------------------------------------------------------------------
// ProcessHistoryChart.xaml.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 07/09/2016, 16:57
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model;
    using RSI.Common.Converters;
    using RSI.Common.Locator;
    using RSI.Common.Mvvm;
    using RSI.Common.Mvvm.Interfaces;
    using RSI.Common.Mvvm.Interfaces.Configuration;
    using RSI.Common.Mvvm.Interfaces.Services;
    using RSI.Common.Tools.Interfaces;
    using RSI.Common.WPFTools.Interfaces.Services;
    using RSI.IndissPlus.Solution;

    /// <summary>
    ///     Interaction logic for ProcessHistoryChart.xaml
    /// </summary>
    [Export("ProcessHistoryChart")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProcessHistoryChart : INotifyPropertyChanged
    {
        /// <summary>
        ///     ChangeSelectedTimeSpanIndex command field declaration
        /// </summary>
        private ICommand _changeSelectedTimeSpanIndexCommand;

        private DateTime _currentPosition;

        private TimeSpan _currentSpan;

        /// <summary>
        ///     NewTrend command field declaration
        /// </summary>
        private RelayCommand _newTrendCommand;

        /// <summary>
        ///     OpenTrend command field declaration
        /// </summary>
        private RelayCommand _openTrendCommand;

        /// <summary>
        ///     PrintTrend command field declaration
        /// </summary>
        private RelayCommand _printTrendCommand;

        /// <summary>
        ///     SaveTrend command field declaration
        /// </summary>
        private RelayCommand _saveTrendCommand;

        /// <summary>
        ///     ScrollX command field declaration
        /// </summary>
        private ICommand _scrollXCommand;

        private TrendDeltaV _trend;

        /// <summary>
        ///     The _visual reference
        /// </summary>
        private object _visualRef;

        public ProcessHistoryChart()
        {
            this.InitializeComponent();

            this._currentSpan = TimeSpan.Zero;

            if (Designer.IsInDesignModeStatic)
            {
                return;
            }

            this.Loaded += (sender, e) => this.DataContext = this;

            // Reload last opened Trend
            this._settings = ServiceContext.ConfigurationService["TrendsDeltaV"];
            var lastOpenedPathfilename = this._settings.GetProperty("LastOpened", string.Empty);
            TrendDeltaV trend;
            if (!string.IsNullOrWhiteSpace(lastOpenedPathfilename) &&
                TrendDeltaV.TryLoad(lastOpenedPathfilename, out trend))
            {
                this.Trend = trend;
            }
        }

        /// <summary>
        ///     Gets the ChangeSelectedTimeSpanIndex command.
        /// </summary>
        /// <value>
        ///     The ChangeSelectedTimeSpanIndex command.
        /// </value>
        public ICommand ChangeSelectedTimeSpanIndexCommand
        {
            get
            {
                return this._changeSelectedTimeSpanIndexCommand
                       ?? (this._changeSelectedTimeSpanIndexCommand =
                           new RelayCommand<string>(this.ExecuteChangeSelectedTimeSpanIndexCommand));
            }
        }

        /// <summary>
        ///     Gets the NewTrend command.
        /// </summary>
        /// <value>
        ///     The NewTrend command.
        /// </value>
        public ICommand NewTrendCommand
        {
            get
            {
                return this._newTrendCommand
                       ?? (this._newTrendCommand =
                           new RelayCommand(this.ExecuteNewTrendCommand));
            }
        }

        /// <summary>
        ///     Gets the OpenTrend command.
        /// </summary>
        /// <value>
        ///     The OpenTrend command.
        /// </value>
        public ICommand OpenTrendCommand
        {
            get
            {
                return this._openTrendCommand
                       ?? (this._openTrendCommand =
                           new RelayCommand(this.ExecuteOpenTrendCommand));
            }
        }

        /// <summary>
        ///     Gets the PrintTrend command.
        /// </summary>
        /// <value>
        ///     The PrintTrend command.
        /// </value>
        public ICommand PrintTrendCommand
        {
            get
            {
                return this._printTrendCommand
                       ?? (this._printTrendCommand =
                           new RelayCommand(
                               () =>
                                   ServiceLocator.Instance.ResolveType<IPrintService>()
                                       .PrintScreenAndPrintCommand.Execute(this),
                               () => this.Trend != null));
            }
        }

        /// <summary>
        ///     Gets the SaveTrend command.
        /// </summary>
        /// <value>
        ///     The SaveTrend command.
        /// </value>
        public ICommand SaveTrendCommand
        {
            get
            {
                return this._saveTrendCommand
                       ?? (this._saveTrendCommand =
                           new RelayCommand(() =>
                           {
                               if (this.Trend.TrySave())
                               {
                                   this._currentXmlTrend = this.Trend.SerializedString;
                               }
                           }, () => this.Trend != null));
            }
        }

        /// <summary>
        ///     Gets the ScrollX command.
        /// </summary>
        /// <value>
        ///     The ScrollX command.
        /// </value>
        public ICommand ScrollXCommand
        {
            get
            {
                return this._scrollXCommand
                       ?? (this._scrollXCommand =
                           new RelayCommand<string>(this.plot.Scroll));
            }
        }

        /// <summary>
        /// EditTrend command field declaration
        /// </summary>
        private RelayCommand _editTrendCommand;

        /// <summary>
        /// Gets the EditTrend command.
        /// </summary>
        /// <value>
        /// The EditTrend command.
        /// </value>
        public ICommand EditTrendCommand
        {
            get
            {
                return this._editTrendCommand
                    ?? (this._editTrendCommand =
                        new RelayCommand(this.ExecuteEditTrendCommand, () => this.Trend != null));
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute Analyse Command].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can execute Analyse Command]; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecuteEditTrendCommand()
        {
            return true;
        }

        /// <summary>
        /// Executes the Analyse Command.
        /// </summary>
        private void ExecuteEditTrendCommand()
        {
            var vm = new TrendConfigViewModel(this.Trend);
            ServiceContext.ObjectWindowManager.OpenModalWindow(vm, true);
        }

        private string _currentXmlTrend;

        /// <summary>
        /// Ensures the current trend saved.
        /// </summary>
        /// <returns></returns>
        public bool EnsureCurrentTrendSaved()
        {
            if (this._trend == null || string.IsNullOrWhiteSpace(this._currentXmlTrend))
            {
                return true;
            }

            if (this._trend.IsNew || this._trend.SerializedString != this._currentXmlTrend)
            {
                switch (
                    ServiceContext.MessageDisplayService.ShowMessage("Confiration required",
                        $"Save changes to {this._trend.Name} ?", null, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question))
                {
                    case MessageBoxResult.Cancel:
                        return false;

                    case MessageBoxResult.Yes:
                        if (!this._trend.TrySave())
                        {
                            return false;
                        }
                        break;
                }

                this._currentXmlTrend = this._trend.SerializedString;
            }
            return true;
        }

        /// <summary>
        ///     Gets or sets the trend.
        /// </summary>
        /// <value>
        ///     The trend.
        /// </value>
        public TrendDeltaV Trend
        {
            get
            {
                return this._trend;
            }

            set
            {
                // Have to Save current trend ?
                if (!this.EnsureCurrentTrendSaved())
                {
                    return;
                }

                var prevTrend = this._trend;
                if (this.Set(ref this._trend, value))
                {
                    if (prevTrend != null)
                    {
                        prevTrend.Dispose();
                    }

                    this._currentXmlTrend = (value != null ? value.SerializedString : null);

                    if (value != null && this._settings != null)
                    {
                        this._settings.SetProperty("LastOpened", value.SourcePathFilename);
                        ServiceContext.ConfigurationService.Save();
                    }
                }
            }
        }

        /// <summary>
        ///     Executes the Analyse Command.
        /// </summary>
        //private void ExecuteApplySet1Command()
        //{
        //    var trendX = new TrendDeltaV("Set 1 ");
        //    TrendDeltaV trend;
        //    if (!TrendDeltaV.TryLoad(trendX.PathFileName, out trend))
        //    {
        //        trend = new TrendDeltaV("Set 1 ");
        //        trend.AddPen("FIC_1003.PV");
        //        trend.AddPen("FIC_1105.PV");
        //        trend.AddPen("FIC_1107.PV");
        //    }
        //    this.Trend = trend;
        //}
        /// <summary>
        ///     Executes the Analyse Command.
        /// </summary>
        private void ExecuteChangeSelectedTimeSpanIndexCommand(string arg)
        {
            if (arg == "1")
            {
                this.SelectedTimeSpanIndex = Math.Min(this.SelectedTimeSpanIndex + 1, 5);
            }
            else
            {
                this.SelectedTimeSpanIndex = Math.Max(this.SelectedTimeSpanIndex - 1, 0);
            }
        }

        /// <summary>
        ///     Executes the Analyse Command.
        /// </summary>
        private void ExecuteNewTrendCommand()
        {
            // Newly created trend will replace current one. Hence, ensure current trend properties are saved
            if (!this.EnsureCurrentTrendSaved())
            {
                return;
            }

            var trend = new TrendDeltaV();
            trend.SetNew();
            var vm = new TrendConfigViewModel(trend);
            ServiceContext.ObjectWindowManager.OpenModalWindow(vm, true);
            if (vm.IsCommitRequired)
            {
                this.Trend = trend;
            }
        }

        /// <summary>
        ///     Executes the Analyse Command.
        /// </summary>
        private void ExecuteOpenTrendCommand()
        {
            var dialog = ServiceLocator.Instance.ResolveType<IFileDialogService>().CreateOpenFileDialog();
            dialog.InitialDirectory = TrendDeltaV.Folder;

            dialog.Filter = "Trends|*.xml|All files (*.*)|*.*";
            dialog.Title = "Trend Browser";
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            TrendDeltaV trend;
            if (TrendDeltaV.TryLoad(dialog.FileName, out trend))
            {
                // Ensure current thread saved
                if (!this.EnsureCurrentTrendSaved())
                {
                    return;
                }

                this.Trend = trend;
            }
        }

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current position span.
        /// </summary>
        /// <value>
        ///     The current position span.
        /// </value>
        public DateTime CurrentPosition
        {
            get
            {
                return this._currentPosition;
            }

            set
            {
                if (this.Set(ref this._currentPosition, value))
                {
                    this.CurrentSpan = this._currentPosition - ServiceContext.TimeService.SimulationStartDate;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the current span.
        /// </summary>
        /// <value>
        ///     The current span.
        /// </value>
        public TimeSpan CurrentSpan
        {
            get
            {
                return this._currentSpan;
            }

            set
            {
                this.Set("CurrentSpan", ref this._currentSpan, value);
            }
        }

        /// <summary>
        ///     Gets or sets the index of the selected time span.
        /// </summary>
        /// <value>
        ///     The index of the selected time span.
        /// </value>
        public int SelectedTimeSpanIndex
        {
            get
            {
                return this._selectedTimeSpanIndex;
            }

            set
            {
                this.Set(ref this._selectedTimeSpanIndex, value);
            }
        }

        private int _selectedTimeSpanIndex;
        private IConfigurationSettings _settings;

        public IEnumerable<string> TimeSpanStrings
        {
            get
            {
                yield return "15 Minutes";
                yield return "30 Minutes";
                yield return "1 Hour";
                yield return "2 Hours";
                yield return "4 Hours";
                yield return "8 Hours";
            }
        }

        /// <summary>
        ///     Gets or sets the visual reference.
        /// </summary>
        /// <value>
        ///     The visual reference.
        /// </value>
        public object VisualRef
        {
            get
            {
                return this._visualRef;
            }

            set
            {
                this.Set("VisualRef", ref this._visualRef, value);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public bool IsPropertyChangedUsed
        {
            get
            {
                return this.PropertyChanged != null;
            }
        }

        private ISynchronizationContext SynchroContext
        {
            get
            {
                return ServiceContext.SynchroContext;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            // ISSUE: reference to a compiler-generated field
            var propertyChanged1 = this.PropertyChanged;
            if (propertyChanged1 == null)
                return;
            if (this.SynchroContext.IsOnUIThread)
                propertyChanged1(this, new PropertyChangedEventArgs(propertyName));
            else
                this.SynchroContext.OnUIThread(p =>
                {
                    // ISSUE: reference to a compiler-generated field
                    var propertyChanged2 = this.PropertyChanged;
                    if (propertyChanged2 == null)
                        return;
                    propertyChanged2(this, new PropertyChangedEventArgs(p));
                }, propertyName);
        }

        private void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            // ISSUE: reference to a compiler-generated field
            if (this.PropertyChanged == null)
                return;
            this.SynchroContext.OnUIThread(p =>
            {
                // ISSUE: reference to a compiler-generated field
                var propertyChanged = this.PropertyChanged;
                if (propertyChanged == null)
                    return;
                var propertyName = PropertyNameViewModel(p);
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }, propertyExpression);
        }

        private static string PropertyNameViewModel<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("expression must be a property expression");
            return body.Member.Name;
        }

        #endregion
    }
}