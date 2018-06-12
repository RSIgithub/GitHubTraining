using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class HARTVlvA_V_TL : RSIControlModel
    {
        static HARTVlvA_V_TL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HARTVlvA_V_TL), new FrameworkPropertyMetadata(typeof(HARTVlvA_V_TL)));
        }
    }
}

