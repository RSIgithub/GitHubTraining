using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_AI_PES : RSIControlModel
    {
        static Serial_AI_PES()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_AI_PES), new FrameworkPropertyMetadata(typeof(Serial_AI_PES)));
        }
    }
}

