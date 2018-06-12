// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AreaFilter.xaml.cs" company="RSI">
//   
// </copyright>
// <summary>
//   Interaction logic for AreaFilter.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RSI.DeltaV.HMI
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for AreaFilter.xaml
    /// </summary>
    [Export("AreaFilter")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AreaFilter
    {
        public AreaFilter()
        {
            this.InitializeComponent();
        }
    }
}