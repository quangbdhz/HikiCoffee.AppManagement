using System;
using System.Globalization;
using System.Windows.Data;

namespace HikiCoffee.AppManager.Converter.ProductConverter
{
    public class ForegroundStockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int stock = System.Convert.ToInt32(value);

            string theme = Properties.Settings.Default.ThemeAppManager;

            if (theme == "Light")
            {
                if (stock < 10)
                    return "Red";
                return "Black";
            }
            else
            {
                if (stock < 10)
                    return "Yellow";
                return "White";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
