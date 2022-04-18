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
            if (value is Person { Days: < 10 })
            {
                return FontWeights.Bold;
            }
            else if (value is Person { Days: < 50 })
            {
                return FontWeights.Bold;
            }

            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
