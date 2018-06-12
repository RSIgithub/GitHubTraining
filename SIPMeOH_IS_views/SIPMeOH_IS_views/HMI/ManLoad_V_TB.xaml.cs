using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class ManLoad_V_TB : RSIControlModel
    {
        static ManLoad_V_TB()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ManLoad_V_TB), new FrameworkPropertyMetadata(typeof(ManLoad_V_TB)));
        }
    }
}

