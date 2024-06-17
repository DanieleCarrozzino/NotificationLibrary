using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NotificationLibrary;

internal class BooleanToBrushConverter : IValueConverter
{
    public Brush TrueValue { get; set; }
    public Brush FalseValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool && (bool)value)
        {
            return TrueValue;
        }

        return FalseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}