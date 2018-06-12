using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TT : RSIControlModel
    {
        static AnalogLink_TT()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TT), new FrameworkPropertyMetadata(typeof(AnalogLink_TT)));
        }
    }
}

