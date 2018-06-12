using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Serial_AO : RSIControlModel
    {
        static Serial_AO()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Serial_AO), new FrameworkPropertyMetadata(typeof(Serial_AO)));
        }
    }
}

