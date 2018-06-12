using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv2 : RSIControlModel
    {
        static Groupv2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv2), new FrameworkPropertyMetadata(typeof(Groupv2)));
        }
    }
}

