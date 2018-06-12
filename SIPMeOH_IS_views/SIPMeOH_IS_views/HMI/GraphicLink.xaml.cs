using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class GraphicLink : RSIControlModel
    {
        static GraphicLink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicLink), new FrameworkPropertyMetadata(typeof(GraphicLink)));
        }
    }
}

