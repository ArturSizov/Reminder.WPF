using Reminder.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Reminder.Converters
{
    public class RowToBrushConverterAge : IValueConverter
    {
        int[] age = new[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 95, 100 };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var a in age)
            {
                if (value is Person person)
                {
                    if (person.Arg == a && person.Days == 0)
                    {
                        return Brushes.Red;
                    }
                }
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
