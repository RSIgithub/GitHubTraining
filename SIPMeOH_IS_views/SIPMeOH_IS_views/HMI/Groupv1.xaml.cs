using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Groupv1 : RSIControlModel
    {
        static Groupv1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Groupv1), new FrameworkPropertyMetadata(typeof(Groupv1)));
        }
    }
}

