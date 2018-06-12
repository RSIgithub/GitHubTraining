// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AI_TR.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for AI_TR.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for AI_TR.xaml
    /// </summary>
    [Export("AI_TR")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AI_TR
    {
        public AI_TR()
        {
            this.InitializeComponent();
        }
    }
}