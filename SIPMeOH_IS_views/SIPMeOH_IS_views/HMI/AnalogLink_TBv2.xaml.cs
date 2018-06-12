using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TBv2 : RSIControlModel
    {
        static AnalogLink_TBv2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TBv2), new FrameworkPropertyMetadata(typeof(AnalogLink_TBv2)));
        }
    }
}

