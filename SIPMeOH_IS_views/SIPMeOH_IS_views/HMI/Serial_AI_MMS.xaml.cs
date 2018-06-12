using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_AI_MMS : RSIControlModel
    {
        static Serial_AI_MMS()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_AI_MMS), new FrameworkPropertyMetadata(typeof(Serial_AI_MMS)));
        }
    }
}

