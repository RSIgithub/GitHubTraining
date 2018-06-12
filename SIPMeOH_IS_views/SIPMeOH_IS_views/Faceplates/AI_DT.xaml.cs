// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AI_DT.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for AI_DT.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for AI_DT.xaml
    /// </summary>
    [Export("AI_DT")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AI_DT
    {
        public AI_DT()
        {
            this.InitializeComponent();
        }
    }
}