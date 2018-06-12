using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv3 : RSIControlModel
    {
        static Groupv3()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv3), new FrameworkPropertyMetadata(typeof(Groupv3)));
        }
    }
}

