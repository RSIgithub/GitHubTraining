// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LOOP_DT.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for LOOP_DT.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for LOOP_DT.xaml
    /// </summary>
    [Export("LOOP_DT")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LOOP_DT
    {
        public LOOP_DT()
        {
            this.InitializeComponent();
        }
    }
}