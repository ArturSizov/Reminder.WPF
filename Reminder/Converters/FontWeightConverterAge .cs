using Reminder.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Reminder.Converters
{
    public class FontWeightConverterAge : IValueConverter
    {
        int[] age = new[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 95, 100 };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Person person)
            {
                foreach (var a in age)
                {
                    if (person.Arg == a && person.Days == 0)
                    {
                        return FontWeights.Bold;
                    }
                }
            }
            return FontWeights.Normal;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
