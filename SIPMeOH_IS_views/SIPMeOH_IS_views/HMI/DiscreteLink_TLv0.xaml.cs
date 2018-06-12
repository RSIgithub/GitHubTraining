using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_TLv0 : RSIControlModel
    {
        static DiscreteLink_TLv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_TLv0), new FrameworkPropertyMetadata(typeof(DiscreteLink_TLv0)));
        }
    }
}

