using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class dynProcHisView : RSIControlModel
    {
        static dynProcHisView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(dynProcHisView), new FrameworkPropertyMetadata(typeof(dynProcHisView)));
        }
    }
}

