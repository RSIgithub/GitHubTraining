using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class Motorv0 : RSIControlModel
    {
        static Motorv0()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Motorv0), new FrameworkPropertyMetadata(typeof(Motorv0)));
        }
    }
}

