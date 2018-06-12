using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class ManLoad_H_TR : RSIControlModel
    {
        static ManLoad_H_TR()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ManLoad_H_TR), new FrameworkPropertyMetadata(typeof(ManLoad_H_TR)));
        }
    }
}

