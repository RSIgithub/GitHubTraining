// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LOOP_FP.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for LOOP_FP.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for LOOP_FP.xaml
    /// </summary>
    [Export("LOOP_FP")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LOOP_FP
    {
        public LOOP_FP()
        {
            this.InitializeComponent();
        }
    }
}