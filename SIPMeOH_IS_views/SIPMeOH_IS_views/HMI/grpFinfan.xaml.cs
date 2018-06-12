using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class grpFinfan : RSIControlModel
    {
        static grpFinfan()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(grpFinfan), new FrameworkPropertyMetadata(typeof(grpFinfan)));
        }
    }
}

