// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterBrowser.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for ParameterBrowser.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for ParameterBrowser.xaml
    /// </summary>
    [Export("ParameterBrowser")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ParameterBrowser
    {
        public ParameterBrowser()
        {
            this.InitializeComponent();
        }
    }
}