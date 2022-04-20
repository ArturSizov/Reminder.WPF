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
            var date = (int)value;

            var color = date <= 10 ? Colors.Red :
                        date <= 50 ? Colors.Orange :
                        Colors.Black;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
