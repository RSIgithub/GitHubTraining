using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_H_DRv0 : RSIControlModel
    {
        static Pump_H_DRv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_H_DRv0), new FrameworkPropertyMetadata(typeof(Pump_H_DRv0)));
        }
    }
}

