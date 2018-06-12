using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv6 : RSIControlModel
    {
        static Groupv6()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv6), new FrameworkPropertyMetadata(typeof(Groupv6)));
        }
    }
}

