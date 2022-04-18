using Reminder.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Reminder.Converters
{
    public class RowToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Person { Days: < 10 })
            {
                return Brushes.Red;
            }
            else if (value is Person { Days: < 50 })
            {
                return Brushes.Orange;
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
