using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MultiMonitorSelect : RSIControlModel
    {
        static MultiMonitorSelect()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiMonitorSelect), new FrameworkPropertyMetadata(typeof(MultiMonitorSelect)));
        }
    }
}

