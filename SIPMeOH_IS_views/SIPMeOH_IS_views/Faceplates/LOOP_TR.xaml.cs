// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LOOP_TR.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for LOOP_TR.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for LOOP_TR.xaml
    /// </summary>
    [Export("LOOP_TR")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LOOP_TR
    {
        public LOOP_TR()
        {
            this.InitializeComponent();
        }
    }
}