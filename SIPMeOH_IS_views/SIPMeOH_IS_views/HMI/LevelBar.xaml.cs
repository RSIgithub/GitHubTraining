using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class LevelBar : RSIControlModel
    {
        static LevelBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LevelBar), new FrameworkPropertyMetadata(typeof(LevelBar)));
        }
    }
}

