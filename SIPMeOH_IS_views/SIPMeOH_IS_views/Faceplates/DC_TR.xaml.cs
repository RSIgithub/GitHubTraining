// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DC_TR.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for DC_TR.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.Faceplates
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for DC_TR.xaml
    /// </summary>
    [Export("DC_TR")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DC_TR
    {
        public DC_TR()
        {
            this.InitializeComponent();
        }
    }
}