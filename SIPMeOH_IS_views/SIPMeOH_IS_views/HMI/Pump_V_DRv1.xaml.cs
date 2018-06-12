using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_V_DRv1 : RSIControlModel
    {
        static Pump_V_DRv1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_V_DRv1), new FrameworkPropertyMetadata(typeof(Pump_V_DRv1)));
        }
    }
}

