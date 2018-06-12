using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Motor : RSIControlModel
    {
        static Motor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Motor), new FrameworkPropertyMetadata(typeof(Motor)));
        }
    }
}

