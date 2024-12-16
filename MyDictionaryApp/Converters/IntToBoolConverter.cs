

using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MyDictionaryApp.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is string paramValue && int.TryParse(paramValue, out var targetValue))
            {
                return intValue == targetValue;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue && parameter is string paramValue && int.TryParse(paramValue, out var targetValue))
            {
                return targetValue;
            }

            return Avalonia.Data.BindingOperations.DoNothing;
        }
    }
}
