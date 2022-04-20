using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Reminder.Converters
{
    public class RowToBrushMultiConverterAge : IMultiValueConverter
    {
        int[] age = new[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 95, 100 };
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var agePerson = (int)values[0];
            var remainingDays = (int)values[1];

            foreach (var a in age)
            {
                if (agePerson == a && remainingDays == 0)
                {
                    return Brushes.Red;
                }
            }
            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
