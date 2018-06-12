using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TTv0 : RSIControlModel
    {
        static AnalogLink_TTv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TTv0), new FrameworkPropertyMetadata(typeof(AnalogLink_TTv0)));
        }
    }
}

