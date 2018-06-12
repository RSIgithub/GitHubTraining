// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrendConfig.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for TrendConfig.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for TrendConfig.xaml
    /// </summary>
    [Export("TrendConfig")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TrendConfig
    {
        public TrendConfig()
        {
            this.InitializeComponent();
        }
    }
}