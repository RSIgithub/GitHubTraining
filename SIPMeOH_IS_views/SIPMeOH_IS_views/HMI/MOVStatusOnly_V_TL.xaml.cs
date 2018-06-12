using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class MOVStatusOnly_V_TL : RSIControlModel
    {
        static MOVStatusOnly_V_TL()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MOVStatusOnly_V_TL), new FrameworkPropertyMetadata(typeof(MOVStatusOnly_V_TL)));
        }
    }
}

