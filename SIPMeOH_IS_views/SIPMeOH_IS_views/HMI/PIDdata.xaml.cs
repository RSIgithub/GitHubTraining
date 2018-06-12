using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class PIDdata : RSIControlModel
    {
        static PIDdata()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PIDdata), new FrameworkPropertyMetadata(typeof(PIDdata)));
        }
    }
}

