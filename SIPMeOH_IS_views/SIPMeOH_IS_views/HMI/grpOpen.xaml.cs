using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class grpOpen : RSIControlModel
    {
        static grpOpen()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(grpOpen), new FrameworkPropertyMetadata(typeof(grpOpen)));
        }
    }
}

