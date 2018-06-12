using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Static_CtrlVlv : RSIControlModel
    {
        static Static_CtrlVlv()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Static_CtrlVlv), new FrameworkPropertyMetadata(typeof(Static_CtrlVlv)));
        }
    }
}

