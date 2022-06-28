using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HikiCoffee.AppManager.Views.ProductViews
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        public AddProductView()
        {
            InitializeComponent();
        }

        private void sc_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
