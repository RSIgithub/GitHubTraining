// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisconnectedScreen.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DisconnectedScreen.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DisconnectedScreen.xaml
    /// </summary>
    [Export("DisconnectedScreen")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DisconnectedScreen
    {
        public DisconnectedScreen()
        {
            this.InitializeComponent();
        }
    }
}