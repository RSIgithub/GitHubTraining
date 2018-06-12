using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class grpDetVariables : RSIControlModel
    {
        static grpDetVariables()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(grpDetVariables), new FrameworkPropertyMetadata(typeof(grpDetVariables)));
        }
    }
}

