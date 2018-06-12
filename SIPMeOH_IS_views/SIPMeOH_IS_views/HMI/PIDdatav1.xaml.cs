using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class PIDdatav1 : RSIControlModel
    {
        static PIDdatav1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PIDdatav1), new FrameworkPropertyMetadata(typeof(PIDdatav1)));
        }
    }
}

