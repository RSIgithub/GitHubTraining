using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MOV_V_TR : RSIControlModel
    {
        static MOV_V_TR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MOV_V_TR), new FrameworkPropertyMetadata(typeof(MOV_V_TR)));
        }
    }
}

