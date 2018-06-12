// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DC_DT.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DC_DT.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DC_DT.xaml
    /// </summary>
    [Export("DC_DT")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DC_DT
    {
        public DC_DT()
        {
            this.InitializeComponent();
        }
    }
}