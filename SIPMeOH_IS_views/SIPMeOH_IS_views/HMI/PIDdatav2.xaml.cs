using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class PIDdatav2 : RSIControlModel
    {
        static PIDdatav2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PIDdatav2), new FrameworkPropertyMetadata(typeof(PIDdatav2)));
        }
    }
}

