using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class PIDdatav0 : RSIControlModel
    {
        static PIDdatav0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PIDdatav0), new FrameworkPropertyMetadata(typeof(PIDdatav0)));
        }
    }
}

