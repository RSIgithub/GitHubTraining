//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file=TrendViewDataGrid.cs" company="RSI">
//    Copyright (c) RSI - All Rights Reserved
//  </copyright>
//  <summary>
//    
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System.Windows.Input;
    using RSI.Controls.Grid;

    public class TrendViewDataGrid : DataGridRsi
    {
        public TrendViewDataGrid()
        {
            this.SelectionUnit = DataGridRsiSelectionUnit.FullRow;
            this.SelectionMode = DataGridSelectionMode.Single;
        }

        protected override void OnCanExecuteBeginEdit(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }

    }
}