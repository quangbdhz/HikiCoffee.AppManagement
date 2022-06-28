using System;
using System.Globalization;
using System.Windows.Data;

namespace HikiCoffee.AppManager.Converter
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string number = value.ToString();
            decimal text = Decimal.Parse(number);
            string newValue = text.ToString("N0");

            return newValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
