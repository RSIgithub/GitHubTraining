// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AI_FP.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for AI_FP.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for AI_FP.xaml
    /// </summary>
    [Export("AI_FP")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AI_FP
    {
        public AI_FP()
        {
            this.InitializeComponent();
        }
    }
}