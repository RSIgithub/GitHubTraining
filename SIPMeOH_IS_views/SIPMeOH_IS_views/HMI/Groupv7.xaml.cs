using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv7 : RSIControlModel
    {
        static Groupv7()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv7), new FrameworkPropertyMetadata(typeof(Groupv7)));
        }
    }
}

