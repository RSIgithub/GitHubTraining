using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Totaliser : RSIControlModel
    {
        static Totaliser()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Totaliser), new FrameworkPropertyMetadata(typeof(Totaliser)));
        }
    }
}

