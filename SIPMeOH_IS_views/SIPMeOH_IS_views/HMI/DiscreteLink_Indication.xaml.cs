using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_Indication : RSIControlModel
    {
        static DiscreteLink_Indication()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_Indication), new FrameworkPropertyMetadata(typeof(DiscreteLink_Indication)));
        }
    }
}

