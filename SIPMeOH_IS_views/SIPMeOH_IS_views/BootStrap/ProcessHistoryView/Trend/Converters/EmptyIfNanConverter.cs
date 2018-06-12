//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file=EmptyIfNaNConverter.cs" company="RSI">
//    Copyright (c) RSI - All Rights Reserved
//  </copyright>
//  <summary>
//    
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Converters
{
    using System;
    using System.Globalization;
    using RSI.Common.Converters;

    /// <summary>
    /// 
    /// </summary>
    public class EmptyIfNaNConverter : ValueConverter<EmptyIfNaNConverter>
    {
        /// <summary>
        /// Converts the implementation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        protected override object ConvertImpl(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;
            if (DoubleUtil.IsNaN(doubleValue))
            {
                return string.Empty;
            }

            return parameter != null ? doubleValue.ToString(parameter.ToString(), culture) : value;
        }
    }
}