using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class ContVlv_V_TB : RSIControlModel
    {
        static ContVlv_V_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContVlv_V_TB), new FrameworkPropertyMetadata(typeof(ContVlv_V_TB)));
        }
    }
}

