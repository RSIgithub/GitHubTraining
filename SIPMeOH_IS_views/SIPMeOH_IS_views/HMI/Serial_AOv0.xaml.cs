using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_AOv0 : RSIControlModel
    {
        static Serial_AOv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_AOv0), new FrameworkPropertyMetadata(typeof(Serial_AOv0)));
        }
    }
}

