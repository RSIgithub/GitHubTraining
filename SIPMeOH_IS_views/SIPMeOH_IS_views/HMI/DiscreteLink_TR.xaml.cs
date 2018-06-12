using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class DiscreteLink_TR : RSIControlModel
    {
        static DiscreteLink_TR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiscreteLink_TR), new FrameworkPropertyMetadata(typeof(DiscreteLink_TR)));
        }
    }
}

