using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class CtrlVlv_V_TB : RSIControlModel
    {
        static CtrlVlv_V_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CtrlVlv_V_TB), new FrameworkPropertyMetadata(typeof(CtrlVlv_V_TB)));
        }
    }
}

