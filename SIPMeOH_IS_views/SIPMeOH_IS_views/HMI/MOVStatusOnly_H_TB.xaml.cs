using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MOVStatusOnly_H_TB : RSIControlModel
    {
        static MOVStatusOnly_H_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MOVStatusOnly_H_TB), new FrameworkPropertyMetadata(typeof(MOVStatusOnly_H_TB)));
        }
    }
}

