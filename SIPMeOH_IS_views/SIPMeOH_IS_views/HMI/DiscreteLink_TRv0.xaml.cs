using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_TRv0 : RSIControlModel
    {
        static DiscreteLink_TRv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_TRv0), new FrameworkPropertyMetadata(typeof(DiscreteLink_TRv0)));
        }
    }
}

