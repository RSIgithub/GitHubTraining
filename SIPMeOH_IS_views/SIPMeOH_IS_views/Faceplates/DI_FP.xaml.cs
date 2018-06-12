// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DI_FP.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DI_FP.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DI_FP.xaml
    /// </summary>
    [Export("DI_FP")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DI_FP
    {
        public DI_FP()
        {
            this.InitializeComponent();
        }
    }
}