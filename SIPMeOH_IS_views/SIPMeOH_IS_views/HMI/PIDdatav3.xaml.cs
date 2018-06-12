using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class PIDdatav3 : RSIControlModel
    {
        static PIDdatav3()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PIDdatav3), new FrameworkPropertyMetadata(typeof(PIDdatav3)));
        }
    }
}

