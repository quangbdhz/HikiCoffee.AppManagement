using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HikiCoffee.AppManager.Views.MainViews.Pages
{
    /// <summary>
    /// Interaction logic for ImportProductPage.xaml
    /// </summary>
    public partial class ImportProductPage : Page
    {
        public ImportProductPage()
        {
            InitializeComponent();
        }

        private void LvImportProuctScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
