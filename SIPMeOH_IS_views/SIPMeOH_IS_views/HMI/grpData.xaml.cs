using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class grpData : RSIControlModel
    {
        static grpData()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(grpData), new FrameworkPropertyMetadata(typeof(grpData)));
        }
    }
}

