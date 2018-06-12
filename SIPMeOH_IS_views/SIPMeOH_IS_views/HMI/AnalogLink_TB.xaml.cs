using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TB : RSIControlModel
    {
        static AnalogLink_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TB), new FrameworkPropertyMetadata(typeof(AnalogLink_TB)));
        }
    }
}

