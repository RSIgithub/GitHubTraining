using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_VLV_H_TB : RSIControlModel
    {
        static Serial_VLV_H_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_VLV_H_TB), new FrameworkPropertyMetadata(typeof(Serial_VLV_H_TB)));
        }
    }
}

