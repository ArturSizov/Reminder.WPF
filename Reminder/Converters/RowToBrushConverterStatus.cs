using Reminder.Models;
using Reminder.Resources;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Reminder.Converters
{
    public class RowToBrushConverterStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Report report)
            {
                if (report.Status == Dict.Translate(Dict.Parameter.Status_yes))
                {
                    return Brushes.LimeGreen;
                }
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
