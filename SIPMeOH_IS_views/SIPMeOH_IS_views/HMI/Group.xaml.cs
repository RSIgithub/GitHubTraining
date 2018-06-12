using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Group : RSIControlModel
    {
        static Group()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Group), new FrameworkPropertyMetadata(typeof(Group)));
        }
    }
}

