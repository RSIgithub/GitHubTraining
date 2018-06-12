using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Pump_H_DRv1 : RSIControlModel
    {
        static Pump_H_DRv1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pump_H_DRv1), new FrameworkPropertyMetadata(typeof(Pump_H_DRv1)));
        }
    }
}

