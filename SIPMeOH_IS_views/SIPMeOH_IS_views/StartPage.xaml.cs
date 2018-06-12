// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartPage.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for StartPage.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    [Export("StartPage")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StartPage
    {
        public StartPage()
        {
            this.InitializeComponent();
        }
    }
}