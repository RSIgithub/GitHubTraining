using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class CtrlVlv_H_TL : RSIControlModel
    {
        static CtrlVlv_H_TL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CtrlVlv_H_TL), new FrameworkPropertyMetadata(typeof(CtrlVlv_H_TL)));
        }
    }
}

