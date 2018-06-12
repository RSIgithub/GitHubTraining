using System.Windows;
using System.Windows.Controls;
using RSI.Emulation.Controls.Control;

namespace SIPMeOH_IS_views.HMI
{
    public class RespectiveProcHisView : RSIControlModel
    {
        static RespectiveProcHisView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RespectiveProcHisView), new FrameworkPropertyMetadata(typeof(RespectiveProcHisView)));
        }
    }
}

