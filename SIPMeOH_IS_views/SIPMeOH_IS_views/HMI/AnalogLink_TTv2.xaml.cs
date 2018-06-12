using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TTv2 : RSIControlModel
    {
        static AnalogLink_TTv2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TTv2), new FrameworkPropertyMetadata(typeof(AnalogLink_TTv2)));
        }
    }
}

