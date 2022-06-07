using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace HikiCoffee.AppManager.Converter
{
    public class StatusTableFurnitureImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? status = value as string;

            string? pathProject = System.Environment.CurrentDirectory;

            string replaceePathProject = pathProject.Replace("bin\\Debug\\net6.0-windows", "");

            if (status == "0")
            {
                //https://res.cloudinary.com/https-deptraitd-blogspot-com/image/upload/v1648398535/HikiCoffee/App_Manager/status_table_1_ywspa2.jpg
                return replaceePathProject + "Images\\TableFurniture\\status_table_0.jpg";
            }
            else
            {
                //https://res.cloudinary.com/https-deptraitd-blogspot-com/image/upload/v1648398537/HikiCoffee/App_Manager/status_table_0_lvu91t.jpg
                return replaceePathProject + "Images\\TableFurniture\\status_table_1.jpg";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
