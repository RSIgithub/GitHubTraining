using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv5 : RSIControlModel
    {
        static Groupv5()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv5), new FrameworkPropertyMetadata(typeof(Groupv5)));
        }
    }
}

