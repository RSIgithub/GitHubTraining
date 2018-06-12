// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DC_FP.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DC_FP.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DC_FP.xaml
    /// </summary>
    [Export("DC_FP")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DC_FP
    {
        public DC_FP()
        {
            this.InitializeComponent();
        }
    }
}