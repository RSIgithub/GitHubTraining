using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MOV_H_TB : RSIControlModel
    {
        static MOV_H_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MOV_H_TB), new FrameworkPropertyMetadata(typeof(MOV_H_TB)));
        }
    }
}

