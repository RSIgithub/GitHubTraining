using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MOVStatusOnly_V_TR : RSIControlModel
    {
        static MOVStatusOnly_V_TR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MOVStatusOnly_V_TR), new FrameworkPropertyMetadata(typeof(MOVStatusOnly_V_TR)));
        }
    }
}

