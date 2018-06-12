using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_VLV_V_TR : RSIControlModel
    {
        static Serial_VLV_V_TR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_VLV_V_TR), new FrameworkPropertyMetadata(typeof(Serial_VLV_V_TR)));
        }
    }
}

