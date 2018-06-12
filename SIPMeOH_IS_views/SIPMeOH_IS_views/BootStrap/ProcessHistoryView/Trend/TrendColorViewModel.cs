// ---------------------------------------------------------------------------------------------
// TrendColorViewModel.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 12/09/2016, 17:18
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend
{
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls;
    using RSI.Common.Mvvm;

    public class TrendColorViewModel : WindowViewModel
    {
        private TrendColor _trendColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrendColorViewModel"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public TrendColorViewModel(TrendColor color)
        {
            this._trendColor = color;
            this._title = "Set Color";
        }

        public override bool CanResize => false;

        public override string Id => "DeltaVTrendColor";

        /// <summary>
        ///     Gets or sets the color of the trend.
        /// </summary>
        /// <value>
        ///     The color of the trend.
        /// </value>
        public TrendColor TrendColor
        {
            get
            {
                return this._trendColor;
            }

            set
            {
                this.Set(ref this._trendColor, value);
            }
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
        }
    }
}