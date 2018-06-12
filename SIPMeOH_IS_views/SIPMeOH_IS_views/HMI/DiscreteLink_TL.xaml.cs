using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_TL : RSIControlModel
    {
        static DiscreteLink_TL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_TL), new FrameworkPropertyMetadata(typeof(DiscreteLink_TL)));
        }
    }
}

