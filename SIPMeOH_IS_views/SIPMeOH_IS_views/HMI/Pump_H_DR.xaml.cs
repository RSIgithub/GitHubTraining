using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_H_DR : RSIControlModel
    {
        static Pump_H_DR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_H_DR), new FrameworkPropertyMetadata(typeof(Pump_H_DR)));
        }
    }
}

