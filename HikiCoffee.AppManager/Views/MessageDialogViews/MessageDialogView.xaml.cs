using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HikiCoffee.AppManager.Views.MessageDialogViews
{
    /// <summary>
    /// Interaction logic for MessageDialogView.xaml
    /// </summary>
    public partial class MessageDialogView : Window
    {
        public MessageDialogView(string text, int option)
        {
            InitializeComponent();

            string urlImageMessage = "";

            if (option == 0)
            {
                urlImageMessage = "https://res.cloudinary.com/https-deptraitd-blogspot-com/image/upload/v1654703418/HikiCoffee/App_Manager/Success_hks6xr.png";
            }
            else if (option == 1)
            {

                urlImageMessage = "https://res.cloudinary.com/https-deptraitd-blogspot-com/image/upload/v1654703418/HikiCoffee/App_Manager/Error_lz8gkt.png";
            }
            else if(option == 2)
            {

                urlImageMessage = "https://res.cloudinary.com/https-deptraitd-blogspot-com/image/upload/v1654703418/HikiCoffee/App_Manager/Warning_qjsni1.png";
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(urlImageMessage);
            bitmap.EndInit();

            img_message.Source = bitmap;

            tb_message.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
