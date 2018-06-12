using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class grpLevelBar : RSIControlModel
    {
        static grpLevelBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(grpLevelBar), new FrameworkPropertyMetadata(typeof(grpLevelBar)));
        }
    }
}

