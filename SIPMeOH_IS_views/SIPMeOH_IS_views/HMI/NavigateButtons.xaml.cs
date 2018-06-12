using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class NavigateButtons : RSIControlModel
    {
        static NavigateButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigateButtons), new FrameworkPropertyMetadata(typeof(NavigateButtons)));
        }
    }
}

