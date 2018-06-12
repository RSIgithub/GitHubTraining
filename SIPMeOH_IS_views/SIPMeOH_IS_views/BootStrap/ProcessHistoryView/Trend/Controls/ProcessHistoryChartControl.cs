// ---------------------------------------------------------------------------------------------
// ProcessHistoryChartControl.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 15/09/2016, 17:57
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model;
    using Iocomp.Common.Iocomp.Classes;
    using PlotComponent.Interfaces;
    using RSI.Common.Converters;
    using RSI.Common.Converters.TypeConverter;
    using RSI.Common.Logger;
    using RSI.Common.Mvvm;
    using RSI.IndissPlus.Plots;
    using RSI.IndissPlus.Plots.Trend;

    /// <summary>
    ///     The Trend Control for Centum VP emulation
    /// </summary>
    /// <seealso cref="RSI.IndissPlus.Plots.Trend.TrendControl" />
    /// <seealso cref="PlotComponent.Interfaces.IVisitor" />
    public class ProcessHistoryChartControl : TrendControl, IVisitor
    {
        /// <summary>
        ///     CursorPosition Dependency Property
        /// </summary>
        public static readonly DependencyProperty CursorPositionProperty;

        /// <summary>
        ///     HorizontalScaleIndex Dependency Property
        /// </summary>
        public static readonly DependencyProperty HorizontalScaleIndexProperty;

        /// <summary>
        ///     Pens Dependency Property
        /// </summary>
        public static readonly DependencyProperty PensProperty;

        /// <summary>
        ///     The default index horizontal.
        /// </summary>
        private const int DefaultIndexHorizontal = 0;

        /// <summary>
        ///     Pens Property name definition
        /// </summary>
        private const string PensPropertyName = "Pens";

        /// <summary>
        ///     The EnlargeXScale Routed UI Command
        /// </summary>
        private static readonly RoutedUICommand EnlargeXScale;

        /// <summary>
        ///     The horizontal scales
        /// </summary>
        private static readonly Dictionary<int, string> HorizontalScales;

        /// <summary>
        ///     The ReduceXScale Routed UI Command
        /// </summary>
        private static readonly RoutedUICommand ReduceXScale;

        /// <summary>
        ///     The vertical scales
        /// </summary>
        private static readonly VerticalScale[] VerticalScales;

        /// <summary>
        ///     The _pens to channels
        /// </summary>
        private readonly Dictionary<PenDeltaV, TrendChannel> _pensToChannels;

        /// <summary>
        ///     The _controller service callback.
        /// </summary>
        // ReSharper disable once NotAccessedField.Local
        private ControllerServiceCallback _controllerServiceCallback;

        /// <summary>
        ///     DisplayInitializationCommand command field declaration
        /// </summary>
        private ICommand _displayInitializationCommand;

        /// <summary>
        ///     EnlargeYScale command field declaration
        /// </summary>
        private RelayCommand _enlargeYScaleCommand;

        /// <summary>
        ///     IndexInitialization command field declaration
        /// </summary>
        private ICommand _indexInitializationCommand;

        private bool _isStaticCursor;

        private double _oldPosition;

        /// <summary>
        ///     ReduceYScale command field declaration
        /// </summary>
        private RelayCommand _reduceYScaleCommand;

        private int _verticalScaleIndex;

        /// <summary>
        ///     Initializes static members of the <see cref="ProcessHistoryChartControl" /> class.
        /// </summary>
        static ProcessHistoryChartControl()
        {
            ReduceXScale = new RoutedUICommand("ReduceXScale", "ReduceXScale", typeof(ProcessHistoryChartControl));
            EnlargeXScale = new RoutedUICommand("EnlargeXScale", "EnlargeXScale", typeof(ProcessHistoryChartControl));

            HorizontalScaleIndexProperty = DependencyProperty.Register(
                "HorizontalScaleIndex",
                typeof(int),
                typeof(ProcessHistoryChartControl),
                new FrameworkPropertyMetadata(
                    DefaultIndexHorizontal,
                    (sender, e) => ((ProcessHistoryChartControl)sender).OnHorizontalScaleIndexChanged((int)e.NewValue),
                    CoerceHorizontalScaleIndexValue));

            HorizontalScales = new Dictionary<int, string>
            {
                { 0, TimeSpan.FromMinutes(15).ToString() },
                { 1, TimeSpan.FromMinutes(30).ToString() },
                { 2, TimeSpan.FromHours(1).ToString() },
                { 3, TimeSpan.FromHours(2).ToString() },
                { 4, TimeSpan.FromHours(4).ToString() },
                { 5, TimeSpan.FromHours(8).ToString() }
            };

            CursorPositionProperty = DependencyProperty.Register(
                "CursorPosition",
                typeof(DateTime),
                typeof(ProcessHistoryChartControl),
                new FrameworkPropertyMetadata(DateTime.MinValue));

            PensProperty = DependencyProperty.Register(
                PensPropertyName,
                typeof(IEnumerable<PenDeltaV>),
                typeof(ProcessHistoryChartControl),
                new FrameworkPropertyMetadata(
                    null,
                    (d, e) =>
                        ((ProcessHistoryChartControl)d).OnPensChanged((IEnumerable<PenDeltaV>)e.OldValue,
                            (IEnumerable<PenDeltaV>)e.NewValue)));

            VerticalScales = new[]
            {
                // new VerticalScale(-100, 200, 800, 100),

                // 0% to 100%
                new VerticalScale(-50, 200, 100, 10),

                // 25% to 75%
                new VerticalScale(-25, 100, 50, 5),

                // 40% to 60%
                new VerticalScale(-10, 40, 20, 2),

                // 45% to 55%
                new VerticalScale(-5, 20, 10, 1)
            };
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessHistoryChartControl" /> class.
        /// </summary>
        public ProcessHistoryChartControl()
        {
            this.InitCommonProperties();
            this.CreateCommandBindings();

            // Add a row to the Grid in order to set horizontal Scroll bar
            this.RowDefinitions.Add(new RowDefinition());
            this.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            this.ColumnDefinitions.Add(new ColumnDefinition());
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // ---------------------------------------
            // Create the Vertical scrollbar
            this.VerticalScrollBar = new ScrollBar
            {
                Orientation = Orientation.Vertical,
                Visibility = Visibility.Collapsed
            };

            // ReSharper disable once RedundantNameQualifier
            this.VerticalScrollBar.SetValue(ColumnProperty, 1);

            // Add to grid
            this.Children.Add(this.VerticalScrollBar);

            // Only if not editing
            if (Designer.IsInDesignModeStatic == false)
            {
                this.VerticalScrollBar.ValueChanged += this.OnVerticalScrollbarValueChanged;
                this._controllerServiceCallback = new ControllerServiceCallback(this);
                this.OnHorizontalScaleIndexChanged(DefaultIndexHorizontal);
            }

            this._pensToChannels = new Dictionary<PenDeltaV, TrendChannel>();
            this._oldPosition = double.MinValue;
            this.Channels = new ObservableCollection<PlotItemControl>();

            //this.BackgoundColor = Color.FromRgb(223, 228, 232);
            this.DataViewColor = Colors.LightGray;
            //this.LinesColor = Color.FromRgb(44, 68, 154);
            this.AxisColor = Colors.Black;

            this.AxisX.ScaleDisplay.GeneratorAuto.MinorCount = 4;
            this.AxisX.ScaleDisplay.TickMajor.Color = System.Drawing.Color.FromArgb(255, 122, 134, 143);
            this.AxisX.ScaleDisplay.TickMinor.Color = System.Drawing.Color.FromArgb(255, 122, 134, 143);
            this.AxisX.SpanChanged += this.OnSpanChanged;
            this.AxisX.MinimumChanged += this.OnMinimumChanged;
            this.AxisX.IsEnabled = true;
            this.UseCommonYAxis = false;

            this.CanControlAxes = false;
            // this.BorderMargin = 8;

            //this.VerticalScrollBar.Value = 50;
            //this.VerticalScaleIndex = 1;

            this.IsTrendManipulationEnabled = true;

            this.IsContextualMenuEnabled = false;
            this.Visitor = this;
        }

        private void OnMinimumChanged(object sender, PlotComponent.Interfaces.Events.DoubleEventArgs e)
        {
            this.AdjustCursorPos();
        }

        private void OnSpanChanged(object sender, EventArgs e)
        {
            this.AdjustCursorPos();
        }

        /// <summary>
        ///     Gets the enlarge x scale command.
        /// </summary>
        public static RoutedUICommand EnlargeXScaleCommand
        {
            get
            {
                return EnlargeXScale;
            }
        }

        /// <summary>
        ///     Gets the reduce x scale command.
        /// </summary>
        public static RoutedUICommand ReduceXScaleCommand
        {
            get
            {
                return ReduceXScale;
            }
        }

        /// <summary>
        ///     Gets or sets the CursorPosition property.  This dependency property
        ///     indicates ....
        /// </summary>
        public DateTime CursorPosition
        {
            get
            {
                return (DateTime)this.GetValue(CursorPositionProperty);
            }
            set
            {
                this.SetValue(CursorPositionProperty, value);
            }
        }

        /// <summary>
        ///     Gets the DisplayInitializationCommand command.
        /// </summary>
        /// <value>
        ///     The DisplayInitializationCommand command.
        /// </value>
        public ICommand DisplayInitializationCommand
        {
            get
            {
                return this._displayInitializationCommand
                       ?? (this._displayInitializationCommand =
                           new RelayCommand(this.ExecuteDisplayInitializationCommand));
            }
        }

        /// <summary>
        ///     Gets the EnlargeYScale command.
        /// </summary>
        /// <value>
        ///     The EnlargeYScale command.
        /// </value>
        public ICommand EnlargeYScaleCommand
        {
            get
            {
                return this._enlargeYScaleCommand
                       ?? (this._enlargeYScaleCommand =
                           new RelayCommand(
                               () => this.VerticalScaleIndex++,
                               () => this.VerticalScaleIndex + 1 < VerticalScales.Length));
            }
        }

        /// <summary>
        ///     Gets or sets the HorizontalScaleIndex property.  This dependency property
        ///     indicates ....
        /// </summary>
        public int HorizontalScaleIndex
        {
            get
            {
                return (int)this.GetValue(HorizontalScaleIndexProperty);
            }

            set
            {
                this.SetValue(HorizontalScaleIndexProperty, value);
            }
        }

        /// <summary>
        ///     Gets the IndexInitialization command.
        /// </summary>
        /// <value>
        ///     The IndexInitialization command.
        /// </value>
        public ICommand IndexInitializationCommand
        {
            get
            {
                return this._indexInitializationCommand
                       ?? (this._indexInitializationCommand = new RelayCommand(this.MoveCursorToLast));
            }
        }

        public bool IsSetCursorOnClickEnabled => true;

        public bool IsStaticCursor
        {
            get
            {
                return this._isStaticCursor;
            }
            set
            {
                if (value != this._isStaticCursor)
                {
                    this._isStaticCursor = value;
                    if (value)
                    {
                    }
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [is vertical scale disabled].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is vertical scale disabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsVerticalScaleDisabled
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the Pens.
        /// </summary>
        /// <value>
        ///     The Pens.
        /// </value>
        public IEnumerable<PenDeltaV> Pens
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get
            {
                return (IEnumerable<PenDeltaV>)this.GetValue(PensProperty);
            }
            set
            {
                this.SetValue(PensProperty, value);
            }
        }

        /// <summary>
        ///     Gets the ReduceYScale command.
        /// </summary>
        /// <value>
        ///     The ReduceYScale command.
        /// </value>
        public ICommand ReduceYScaleCommand
        {
            get
            {
                return this._reduceYScaleCommand
                       ?? (this._reduceYScaleCommand =
                           new RelayCommand(() => this.VerticalScaleIndex--, () => this.VerticalScaleIndex - 1 >= 0));
            }
        }

        /// <summary>
        ///     Gets or sets the index of the vertical scale.
        /// </summary>
        /// <value>
        ///     The index of the vertical scale.
        /// </value>
        public int VerticalScaleIndex
        {
            get
            {
                return this._verticalScaleIndex;
            }
            set
            {
                value = this.CoerceVerticalScaleIndexValue(value);
                if (value != this._verticalScaleIndex)
                {
                    this._verticalScaleIndex = value;
                    this.OnVerticalScaleIndexChanged(value);
                }
            }
        }

        /// <summary>
        ///     The _vertical scroll bar.
        /// </summary>
        protected ScrollBar VerticalScrollBar
        {
            get;
        }

        /// <summary>
        ///     Gets the y axes.
        /// </summary>
        /// <value>
        ///     The y axes.
        /// </value>
        private IEnumerable<ID3YAxis> YAxes
        {
            get
            {
                return this._pensToChannels.Values.Select(channel => channel.TryGetYd3Axis());
            }
        }

        /// <summary>
        ///     Displays the context menu.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void DisplayContextMenu(int x, int y)
        {
        }

        /// <summary>
        ///     Called when [data cursor moved to].
        /// </summary>
        /// <param name="infos">The infos.</param>
        /// <param name="xValue">The x value.</param>
        /// <param name="yValue">The y value.</param>
        public void OnDataCursorMovedTo(IChannelInfos infos, double xValue, double yValue)
        {
            if (DoubleUtil.AreClose(this._oldPosition, xValue) == false)
            {
                this._oldPosition = xValue;
                var positionAsDateTime = Math2.DoubleToDateTime(xValue);
                this.CursorPosition = positionAsDateTime;
            }

            var pen = infos as PenDeltaV;
            if (pen != null)
            {
                pen.ValueAtCursor = yValue;
                var cursorPosition = this.CursorPosition;
                var timeStamp =
                    $"{cursorPosition.Day:00}/{cursorPosition.Month:00}/{cursorPosition.Year - 2000} {cursorPosition.Hour:00}:{cursorPosition.Minute:00}:{cursorPosition.Second:00}";
                pen.TimeStamp = timeStamp;
            }
        }

        /// <summary>
        ///     Processes the simulation progress.
        /// </summary>
        /// <param name="simulatedTime">
        ///     The simulated time.
        /// </param>
        public void ProcessSimulationProgress(TimeSpan simulatedTime)
        {
            this.AdjustCursorPos();
        }

        /// <summary>
        /// Adjusts the cursor position.
        /// </summary>
        private void AdjustCursorPos()
        {
            if (this.IsStaticCursor && DoubleUtil.AreClose(this._oldPosition, double.MinValue) == false)
            {
                this.MoveCursorTo(this._oldPosition);
            }
        }

        /// <summary>
        /// Scrolls the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void Scroll(string target)
        {
            switch (target)
            {
                case "3":
                    this.IsStaticCursor = false;
                    this.MoveCursorToLast();
                    this.IsTracking = true;
                    break;
                case "-2":
                    this.IsTracking = false;
                    this.InternalScrollBackward(TimeSpan.FromMinutes((this.CurrentTimeSpan.TotalMinutes * 9) / 15));
                    break;
                case "-1":
                    this.IsTracking = false;
                    this.InternalScrollBackward(TimeSpan.FromMinutes((this.CurrentTimeSpan.TotalMinutes * 6) / 15));
                    break;
                case "1":
                    this.IsTracking = false;
                    this.InternalScrollForward(TimeSpan.FromMinutes((this.CurrentTimeSpan.TotalMinutes * 6) / 15));
                    break;
                case "2":
                    this.IsTracking = false;
                    this.InternalScrollForward(TimeSpan.FromMinutes((this.CurrentTimeSpan.TotalMinutes * 9) / 15));
                    break;
            }

            this.AdjustCursorPos();
        }

        /// <summary>
        ///     Called when [Pens changed].
        /// </summary>
        /// <param name="oldPens">Old Pens.</param>
        /// <param name="newPens">New Pens.</param>
        protected virtual void OnPensChanged(IEnumerable<PenDeltaV> oldPens, IEnumerable<PenDeltaV> newPens)
        {
            if (this.IsDisposed)
            {
                return;
            }

            // Clear channels
            this.Channels.Clear();
            this._pensToChannels.Clear();

            // Old collection already available ?
            if (oldPens != null)
            {
                foreach (var pen in oldPens)
                {
                    pen.Dispose();
                }

                var oldValues = oldPens as INotifyCollectionChanged;
                if (oldValues != null)
                {
                    oldValues.CollectionChanged -= this.OnPenCollectionChanged;
                }
            }

            // New collection available ?
            if (newPens == null)
            {
                return;
            }

            // Listen to new collection changes if possible
            var oc = newPens as INotifyCollectionChanged;
            if (oc != null)
            {
                // Listen to collection change
                oc.CollectionChanged += this.OnPenCollectionChanged;
            }

            foreach (var pen in newPens)
            {
                this.AddChannel(pen);
            }

            this.ShowCursor = true;
            this.CursorLineStyle = PlotPenStyle.Solid;
            this.CursorColor = "Black";
            this.MoveCursorToLast();
            this.HideHintText();
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var pens = this.Pens as INotifyCollectionChanged;
                if (pens != null)
                {
                    pens.CollectionChanged -= this.OnPenCollectionChanged;
                }
            }

            base.Dispose(isDisposing);
        }

        /// <summary>
        ///     Coerces the HorizontalScaleIndex value.
        /// </summary>
        /// <param name="d">
        ///     The d.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        private static object CoerceHorizontalScaleIndexValue(DependencyObject d, object value)
        {
            var desiredIndex = (int)value;
            desiredIndex = Math.Max(0, desiredIndex);
            desiredIndex = Math.Min(HorizontalScales.Count - 1, desiredIndex);
            return desiredIndex;
        }

        /// <summary>
        ///     Creates the trend channel.
        /// </summary>
        /// <param name="penDeltaV">The pen.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">pen</exception>
        private void AddChannel(PenDeltaV penDeltaV)
        {
            if (penDeltaV == null)
            {
                throw new ArgumentNullException("penDeltaV");
            }

            var trendChannel = new TrendChannel { IsBinded = true, ShowAxis = true, Infos = penDeltaV };

            // Listen to channel color
            BindingOperations.SetBinding(
                trendChannel,
                PlotItemControl.LineStrokeProperty,
                new Binding("Color") { Source = penDeltaV });

            // Listen to Parameter
            BindingOperations.SetBinding(
                trendChannel,
                TrendChannel.ParameterProperty,
                new Binding("Parameter") { Source = penDeltaV });

            // Listen to Max Scale Value
            BindingOperations.SetBinding(
                trendChannel,
                PlotItemControl.YMaxProperty,
                new Binding("MaximumModelValue") { Source = penDeltaV });

            // Listen to Min Scale Value
            BindingOperations.SetBinding(
                trendChannel,
                PlotItemControl.YMinProperty,
                new Binding("MinimumModelValue") { Source = penDeltaV });

            // Attach Channel to Plot
            this.Channels.Add(trendChannel);

            // Y Axis Scale
            var yAxis = trendChannel.TryGetYd3Axis();
            yAxis.GeneratorStyle = ScaleDisplayStyles.Fixed;
            yAxis.ScaleDisplay.GeneratorFixed.MajorCount = 6;
            yAxis.ScaleDisplay.GeneratorFixed.MinorCount = 1;
            yAxis.ScaleDisplay.TextFormatting.Precision = 0;

            // Y Axis Color
            var gridLine = yAxis.AxisGridLine;
            gridLine.MajorLine.Color = System.Drawing.Color.FromArgb(255, 44, 68, 154);
            gridLine.MinorLine.Color = gridLine.MajorLine.Color;
            gridLine.MinorLine.Style = PlotPenStyle.Dot;
            gridLine.MajorLine.Visible = true;
            gridLine.MinorLine.Visible = true;

            // Tick Colors
            yAxis.ScaleDisplay.TickMajor.Color = System.Drawing.Color.FromArgb(255, 122, 134, 143);
            yAxis.ScaleDisplay.TickMajor.Length = 4;
            yAxis.ScaleDisplay.TickMajor.TextMargin = 3;
            yAxis.ScaleDisplay.TickMinor.Color = System.Drawing.Color.FromArgb(255, 122, 134, 143);

            // y Axis Title
            yAxis.AxisTitle.Text = penDeltaV.ParameterName;
            yAxis.AxisTitle.MarginOuter = 1;

            // Rounded Back ground
            //yAxis.RoundColor = Color.FromArgb(255, 26, 31, 40);
            //yAxis.CornerRadius = 8;

            // Add
            this._pensToChannels.Add(penDeltaV, trendChannel);
        }

        /// <summary>
        ///     Coerces the vertical scale index value.
        /// </summary>
        /// <param name="desiredIndex">Index of the desired.</param>
        /// <returns></returns>
        private int CoerceVerticalScaleIndexValue(int desiredIndex)
        {
            desiredIndex = Math.Max(0, desiredIndex);
            desiredIndex = Math.Min(VerticalScales.Length - 1, desiredIndex);
            return desiredIndex;
        }

        /// <summary>
        ///     Creates the command bindings.
        /// </summary>
        private void CreateCommandBindings()
        {
            // REDUCE X SCALE
            this.CommandBindings.Add(
                new CommandBinding(
                    ReduceXScale,
                    (sender, e) => { this.HorizontalScaleIndex--; },
                    (sender, e) => { e.CanExecute = this.HorizontalScaleIndex > 0; }));

            // ENLARGE X SCALE
            this.CommandBindings.Add(
                new CommandBinding(
                    EnlargeXScale,
                    (sender, e) => { this.HorizontalScaleIndex++; },
                    (sender, e) => { e.CanExecute = this.HorizontalScaleIndex < HorizontalScales.Count - 1; }));
        }

        /// <summary>
        ///     Executes the Display Initialization Command.
        /// </summary>
        private void ExecuteDisplayInitializationCommand()
        {
            // Vertical Scale = 100%
            this.VerticalScaleIndex = 1;

            // Vertical Value = 50;
            this.VerticalScrollBar.Value = 50;

            // Horizontal Index = 6 min
            this.HorizontalScaleIndex = DefaultIndexHorizontal;
        }

        /// <summary>
        ///     Initializes the common properties.
        /// </summary>
        private void InitCommonProperties()
        {
            this.ShowLegend = false;
            this.TimeSpan = TimeSpan.FromMinutes(15);

            this.AxisX.ScaleDisplay.TextFormatting.DateTimeFormat = "HH:mm";

            // 1 Minute
            // Math2.TimeSpanToAxisValue(TimeSpan.FromMinutes(1));
            this.AxisX.ScaleDisplay.GeneratorAuto.DesiredIncrement = 0.000694444446708076;
            this.AxisX.ScaleDisplay.GeneratorStyle = ScaleGeneratorStyle.Auto;
        }



        /// <summary>
        ///     Provides derived classes an opportunity to handle changes to the HorizontalScaleIndex property.
        /// </summary>
        /// <param name="newIndex">
        ///     The new Index.
        /// </param>
        private void OnHorizontalScaleIndexChanged(int newIndex)
        {
            string config;
            if (HorizontalScales.TryGetValue(newIndex, out config) == false)
            {
                return;
            }

            var curMax = this.AxisX.Max;
            var items = config.Split(',');
            string desiredSpan;
            if (items[0].TryConvertTo(out desiredSpan))
            {
                this.TimeSpan = TimeSpan.Parse(desiredSpan);
            }

            //if (this.IsStaticCursor)
            //{
            //    //this.MinDateTime = Math2.DoubleToDateTime(this._oldPosition) -
            //    //                   TimeSpan.FromSeconds(this.CurrentTimeSpan.TotalSeconds / 2);
            //}
            //else
            {
                this.MinDateTime = Math2.DoubleToDateTime(curMax) - this.CurrentTimeSpan;
                if (this.IsTracking)
                {
                    this.MoveCursorToLast();
                }
            }

            this.AdjustCursorPos();
        }

        /// <summary>
        ///     Called when [pen collection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void OnPenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var pen in e.NewItems.OfType<PenDeltaV>())
                    {
                        this.AddChannel(pen);
                    }

                    this.ShowCursor = true;
                    this.CursorLineStyle = PlotPenStyle.Solid;
                    this.CursorColor = "Black";
                    this.MoveCursorToLast();
                    this.HideHintText();

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var pen in e.OldItems.OfType<PenDeltaV>())
                    {
                        pen.Dispose();

                        TrendChannel ch;
                        if (this._pensToChannels.TryGetValue(pen, out ch))
                        {
                            this._pensToChannels.Remove(pen);
                            this.Channels.Remove(ch);
                        }
                        else
                        {
                            PerfService.Logger.Error(string.Format("Failed to remove Pen {0}", pen));
                        }
                    }

                    this.ShowCursor = true;
                    this.CursorLineStyle = PlotPenStyle.Solid;
                    this.CursorColor = "Black";
                    this.MoveCursorToLast();
                    this.HideHintText();

                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (var pen in this.Pens)
                    {
                        pen.Dispose();
                    }
                    this.Channels.Clear();
                    this._pensToChannels.Clear();
                    break;
            }
        }

        /// <summary>
        ///     Called when [vertical scale index changed].
        /// </summary>
        /// <param name="index">New index of the vertical scale.</param>
        private void OnVerticalScaleIndexChanged(int index)
        {
            // Apply Vertical Scrollbar properties
            var currentScale = VerticalScales[index];
            currentScale.ApplyTo(this.VerticalScrollBar);

            // Adjust YAxis Min/Max/Span
            var scaleSpanPercent = currentScale.ScaleSpan / 100;
            foreach (var kvp in this._pensToChannels)
            {
                var span = kvp.Key.MaximumModelValue - kvp.Key.MinimumModelValue;
                kvp.Value.TryGetYd3Axis().AdjustSpanUsingMidReference(span * scaleSpanPercent);
            }

            if (this._enlargeYScaleCommand != null)
            {
                this._enlargeYScaleCommand.RaiseCanExecuteChanged();
            }

            if (this._reduceYScaleCommand != null)
            {
                this._reduceYScaleCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        ///     Called when [vertical scrollbar value changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OnVerticalScrollbarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var percentOffset = (e.NewValue - e.OldValue) / 100;
            foreach (var kvp in this._pensToChannels)
            {
                var span = kvp.Key.MaximumModelValue - kvp.Key.MinimumModelValue;
                kvp.Value.TryGetYd3Axis().Min += span * percentOffset;
            }
            // PerfService.Logger.Error("NewValue=" + e.NewValue + "- OldValue=" + e.OldValue);
        }

        private struct VerticalScale
        {
            private readonly double _largeChange;

            private readonly double _minimum;

            private readonly double _smallChange;

            private readonly double _viewportSize;

            /// <summary>
            ///     Initializes a new instance of the <see cref="VerticalScale" /> struct.
            /// </summary>
            /// <param name="minimum">The minimum.</param>
            /// <param name="viewportSize">Size of the viewport.</param>
            /// <param name="largeChange">The large change.</param>
            /// <param name="smallChange">The small change.</param>
            public VerticalScale(double minimum, double viewportSize, double largeChange, double smallChange)
            {
                this._minimum = minimum;
                this._viewportSize = viewportSize;
                this._largeChange = largeChange;
                this._smallChange = smallChange;
            }

            /// <summary>
            ///     Gets the scale span.
            /// </summary>
            /// <value>
            ///     The scale span.
            /// </value>
            public double ScaleSpan
            {
                get
                {
                    return Math.Abs(this._minimum) * 2;
                }
            }

            /// <summary>
            ///     Applies to.
            /// </summary>
            /// <param name="scrollBar">The scroll bar.</param>
            public void ApplyTo(ScrollBar scrollBar)
            {
                scrollBar.Minimum = 0;
                scrollBar.Maximum = 100;
                scrollBar.ViewportSize = this._viewportSize;
                scrollBar.LargeChange = this._largeChange;
                scrollBar.SmallChange = this._smallChange;
            }
        }
    }
}