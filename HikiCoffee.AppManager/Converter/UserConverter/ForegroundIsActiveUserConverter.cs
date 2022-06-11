using System;
using System.Globalization;
using System.Windows.Data;

namespace HikiCoffee.AppManager.Converter.UserConverter
{
    internal class ForegroundIsActiveUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isActive = (bool)value;

                if (isActive)
                    return "#05c46b";
                return "Red";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
