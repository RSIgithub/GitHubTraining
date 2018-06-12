using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class AI_HI_HI_ALARM : RSIControlModel
    {
        static AI_HI_HI_ALARM()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AI_HI_HI_ALARM), new FrameworkPropertyMetadata(typeof(AI_HI_HI_ALARM)));
        }
    }
}

