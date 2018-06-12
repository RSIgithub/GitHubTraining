// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DI_TR.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DI_TR.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DI_TR.xaml
    /// </summary>
    [Export("DI_TR")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DI_TR
    {
        public DI_TR()
        {
            this.InitializeComponent();
        }
    }
}