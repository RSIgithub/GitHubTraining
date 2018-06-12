// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectedScreen.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for ConnectedScreen.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.Composition;

namespace SIPMeOH_IS_views.BootStrap
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for ConnectedScreen.xaml
    /// </summary>
    [Export("ConnectedScreen")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ConnectedScreen
    {
        public ConnectedScreen()
        {
            this.InitializeComponent();
        }
    }
}