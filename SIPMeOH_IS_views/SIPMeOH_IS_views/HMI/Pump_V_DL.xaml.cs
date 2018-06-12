using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_V_DL : RSIControlModel
    {
        static Pump_V_DL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_V_DL), new FrameworkPropertyMetadata(typeof(Pump_V_DL)));
        }
    }
}

