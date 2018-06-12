// ---------------------------------------------------------------------------------------------
// PenDeltaV.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 07/09/2016, 10:53
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model
{
    using System;
    using System.Windows.Input;
    using System.Xml.Serialization;
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls;
    using PlotComponent.Interfaces;
    using RSI.Common.Converters.TypeConverter;
    using RSI.Common.Mvvm;
    using RSI.IndissPlus.Solution;
    using RSI.IndissPlus.Solution.Helpers;
    using RSI.Kernel.Model.Interfaces.Models;
    using RSI.Kernel.Model.Interfaces.Models.Parameters;
    using RSI.Kernel.Model.Models.Parameters;
    using RSI.Services.Recorder.Interfaces;

    /// <summary>
    ///     The pen.
    /// </summary>
    /// <seealso cref="TrendObject" />
    /// <seealso cref="PlotComponent.Interfaces.IChannelInfos" />
    [Serializable]
    [XmlRoot("Pen")]
    public class PenDeltaV : TrendObject, IChannelInfos, IParameterValueChangedAware<string>, IParameterValueChangedAware<double>
    {
        /// <summary>
        ///     The _color.
        /// </summary>
        private string _color;

        /// <summary>
        ///     recorded information
        /// </summary>
        private IRecorderInfos _lazyRecorderInfos;

        private double _maximumValue;

        private double _minimumValue;

        private bool _initDone;

        /// <summary>
        ///     The _parameter
        /// </summary>
        private IObservableItem _parameter;

        IScalarDoubleParameter _minParameter;
        IScalarDoubleParameter _maxParameter;

        /// <summary>
        ///     The _parameter name
        /// </summary>
        private string _parameterName;

        /// <summary>
        ///     The _value at cursor.
        /// </summary>
        private double _valueAtCursor;

        private string _uom;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this._unitParameter != null)
                {
                    this._unitParameter.UnregisterListener(this);
                    this._unitParameter = null;
                }

                if (this._maxParameter != null)
                {
                    this._maxParameter.UnregisterListener(this);
                    this._maxParameter = null;
                }

                if (this._minParameter != null)
                {
                    this._minParameter.UnregisterListener(this);
                    this._minParameter = null;
                }

                if (this._maxCell != null)
                {
                    this._maxCell.PropertyChanged -= this.OnMaxCellPropertyChanged;
                    this._maxCell = null;
                }

                if (this._minCell != null)
                {
                    this._minCell.PropertyChanged -= this.OnMinCellPropertyChanged;
                    this._minCell = null;
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PenDeltaV" /> class.
        /// </summary>
        public PenDeltaV()
        {
            this._minimumValue = 0;
            this._maximumValue = 100;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PenDeltaV" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="color">Channel color</param>
        public PenDeltaV(string parameterName, string color) : this()
        {
            this._parameterName = parameterName;
            this._color = color;
        }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        [XmlAttribute]
        public string Color
        {
            get
            {
                return this._color;
            }

            set
            {
                this.Set(ref this._color, value);
            }
        }

        /// <summary>
        /// OpenColorWindow command field declaration
        /// </summary>
        private RelayCommand _openColorWindowCommand;

        /// <summary>
        /// Gets the OpenColorWindow command.
        /// </summary>
        /// <value>
        /// The OpenColorWindow command.
        /// </value>
        public ICommand OpenColorWindowCommand
        {
            get
            {
                return this._openColorWindowCommand
                    ?? (this._openColorWindowCommand =
                        new RelayCommand(this.ExecuteOpenColorWindowCommand));

            }
        }

        /// <summary>
        /// Executes the Analyse Command.
        /// </summary>
        private void ExecuteOpenColorWindowCommand()
        {
            var vm = new TrendColorViewModel(Colorizer.StringToTrendColor(this.Color));
            ServiceContext.ObjectWindowManager.OpenModalWindow(vm, true);
            if (vm.IsCommitRequired)
            {
                this.Color = Colorizer.EnumToString(vm.TrendColor);
            }
        }

        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        public double MaximumModelValue
        {
            get
            {
                this.EnsureDataCollected();
                return this._maximumValue;
            }
        }

        /// <summary>
        ///     Gets or sets the minimum.
        /// </summary>
        /// <value>
        ///     The minimum.
        /// </value>
        public double MinimumModelValue
        {
            get
            {
                this.EnsureDataCollected();
                return this._minimumValue;
            }
        }

        /// <summary>
        /// Gets the uom.
        /// </summary>
        /// <value>
        /// The uom.
        /// </value>
        public string UOM
        {
            get
            {
                this.EnsureDataCollected();
                return this._uom;
            }
        }

        public int Number => 1;

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public IObservableItem Parameter
        {
            get
            {
                if (this._parameter == null)
                {
                    ServiceContext.ModelService.TryGetParameterFromPath(
                        this._parameterName,
                        out this._parameter);
                }

                return this._parameter;
            }
        }

        /// <summary>
        ///     Gets or sets the id of the parameter (FC101.PV / LC101.SV /...)
        /// </summary>
        /// <value>
        ///     The id of the parameter.
        /// </value>
        [XmlAttribute("Parameter")]
        public string ParameterName
        {
            get
            {
                return this._parameterName;
            }

            set
            {
                this.Set(ref this._parameterName, value);
            }
        }

        ScalarStringParameter _unitParameter;

        /// <summary>
        ///     Gets or sets the value at cursor.
        /// </summary>
        /// <value>
        ///     The value at cursor.
        /// </value>
        [XmlIgnore]
        public double ValueAtCursor
        {
            get
            {
                return this._valueAtCursor;
            }

            set
            {
                this.Set(ref this._valueAtCursor, value);
            }
        }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        [XmlIgnore]
        public string TimeStamp
        {
            get
            {
                return this._timeStamp;
            }

            set
            {
                this.Set(ref this._timeStamp, value);
            }
        }

        private string _timeStamp;

        /// <summary>
        ///     Tries the get recorder information.
        /// </summary>
        /// <param name="recorderInfos">
        ///     The recorder infos.
        /// </param>
        /// <returns>
        ///     Operation status
        /// </returns>
        public bool TryGetRecorderInfo(out IRecorderInfos recorderInfos)
        {
            if (this._lazyRecorderInfos != null)
            {
                recorderInfos = this._lazyRecorderInfos;
                return true;
            }

            var parameter = this.Parameter;
            if (parameter == null)
            {
                recorderInfos = null;
                return false;
            }

            ServiceContext.RecorderService.TryGetRecorderInfos(parameter, out this._lazyRecorderInfos);
            recorderInfos = this._lazyRecorderInfos;
            return recorderInfos != null;
        }

        /// <summary>
        /// Ensures the bounds collected.
        /// </summary>
        private void EnsureDataCollected()
        {
            if (this._initDone)
            {
                return;
            }

            this._initDone = true;

            // get Parameter ref
            var parameter = this.Parameter;
            if (parameter == null)
            {
                return;
            }

            // get Unit operation ref
            var parentUnit = parameter.ParentUnit as IUnit;
            if (parentUnit == null)
            {
                return;
            }

            // get target parameter Id
            var parameterId = this._parameter.GetId();

            // Get target Unit operation type
            var typeName = parentUnit.TypeName;

            // try to get rules based on static dictionary
            string parameterMinId;
            string parameterMaxId;
            string UOM;

            if (parentUnit.TryGetMinMaxAndUOM(parameterId, out parameterMinId, out parameterMaxId,
                out UOM))
            {
                // Unit ?
                if (!string.IsNullOrWhiteSpace(UOM))
                {
                    if (parentUnit.TryGetParameterWithId(UOM, out this._unitParameter))
                    {
                        this._unitParameter.RegisterListener(this);
                    }
                }

                // Are Min & Max directly provided
                if (parameterMinId == "0")
                {
                    parameterMinId.TryConvertTo(out this._minimumValue);
                    parameterMaxId.TryConvertTo(out this._maximumValue);
                    return;
                }

                if (parentUnit.TryGetParameterWithId(parameterMinId, out this._minParameter))
                {
                    this._minParameter.RegisterListener(this);
                }

                if (parentUnit.TryGetParameterWithId(parameterMaxId, out this._maxParameter))
                {
                    this._maxParameter.RegisterListener(this);
                }

                return;

            }

            // Special case : DeltaVSPLTR
            if (typeName == "DeltaVSPLTR")
            {
                IOneDimDoubleParameter array;
                string arrayId;
                int indexLo;
                int indexHi;
                if (parameterId == "SP")
                {
                    indexLo = 0;
                    indexHi = 3;
                    arrayId = "IN_ARRAY";
                }
                else if (parameterId == "OUT_1")
                {
                    indexLo = 0;
                    indexHi = 1;
                    arrayId = "OUT_ARRAY";
                }
                else if (parameterId == "OUT_2")
                {
                    indexLo = 2;
                    indexHi = 3;
                    arrayId = "OUT_ARRAY";
                }
                else
                {
                    return;
                }


                if (parentUnit.TryGetParameterWithId(arrayId, out array) && array.RowCount < indexHi)
                {
                    this._minCell = array[indexLo];
                    this._maxCell = array[indexHi];
                    this._minimumValue = this._minCell.ModelValue;
                    this._maximumValue = this._maxCell.ModelValue;

                    this._minCell.PropertyChanged += this.OnMinCellPropertyChanged;
                    this._maxCell.PropertyChanged += this.OnMaxCellPropertyChanged;
                }
            }
        }

        private void OnMaxCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void OnMinCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private ICell<double> _minCell;
        private ICell<double> _maxCell;

        /// <summary>
        /// Processes the parameter value changed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="newValue">The new value.</param>
        public void ProcessParameterValueChanged(IScalarParameter<string> parameter, string newValue)
        {
            this._uom = newValue;
            this.RaisePropertyChanged(() => this.UOM);
        }

        /// <summary>
        /// Processes the parameter value changed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="newValue">The new value.</param>
        public void ProcessParameterValueChanged(IScalarParameter<double> parameter, double newValue)
        {
            if (parameter == this._maxParameter)
            {
                this._maximumValue = newValue;
                this.RaisePropertyChanged(() => this.MaximumModelValue);
            }
            else if (parameter == this._minParameter)
            {
                this._minimumValue = newValue;
                this.RaisePropertyChanged(() => this.MinimumModelValue);
            }
        }
    }
}