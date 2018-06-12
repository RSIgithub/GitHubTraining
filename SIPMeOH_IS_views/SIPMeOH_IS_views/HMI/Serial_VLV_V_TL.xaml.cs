using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_VLV_V_TL : RSIControlModel
    {
        static Serial_VLV_V_TL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_VLV_V_TL), new FrameworkPropertyMetadata(typeof(Serial_VLV_V_TL)));
        }
    }
}

