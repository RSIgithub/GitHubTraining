using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class GraphicLinkv0 : RSIControlModel
    {
        static GraphicLinkv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicLinkv0), new FrameworkPropertyMetadata(typeof(GraphicLinkv0)));
        }
    }
}

