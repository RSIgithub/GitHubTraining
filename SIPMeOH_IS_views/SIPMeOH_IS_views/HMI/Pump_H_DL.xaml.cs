using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_H_DL : RSIControlModel
    {
        static Pump_H_DL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_H_DL), new FrameworkPropertyMetadata(typeof(Pump_H_DL)));
        }
    }
}

