using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_TRv1 : RSIControlModel
    {
        static DiscreteLink_TRv1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_TRv1), new FrameworkPropertyMetadata(typeof(DiscreteLink_TRv1)));
        }
    }
}

