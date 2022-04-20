using Reminder.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Reminder.Converters
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (int)value;

            var font = date <= 50 ? FontWeights.Bold:
                        FontWeights.Normal;

            return font;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
