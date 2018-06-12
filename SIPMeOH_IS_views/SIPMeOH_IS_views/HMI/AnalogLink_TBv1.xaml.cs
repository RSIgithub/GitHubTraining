using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AnalogLink_TBv1 : RSIControlModel
    {
        static AnalogLink_TBv1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogLink_TBv1), new FrameworkPropertyMetadata(typeof(AnalogLink_TBv1)));
        }
    }
}

