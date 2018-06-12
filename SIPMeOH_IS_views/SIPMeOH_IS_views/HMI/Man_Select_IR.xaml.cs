using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Man_Select_IR : RSIControlModel
    {
        static Man_Select_IR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Man_Select_IR), new FrameworkPropertyMetadata(typeof(Man_Select_IR)));
        }
    }
}

