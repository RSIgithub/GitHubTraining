using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv4 : RSIControlModel
    {
        static Groupv4()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv4), new FrameworkPropertyMetadata(typeof(Groupv4)));
        }
    }
}

