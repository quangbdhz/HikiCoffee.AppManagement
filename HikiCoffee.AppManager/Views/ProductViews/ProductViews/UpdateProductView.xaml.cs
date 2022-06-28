using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HikiCoffee.AppManager.Views.ProductViews.ProductViews
{
    /// <summary>
    /// Interaction logic for UpdateProductView.xaml
    /// </summary>
    public partial class UpdateProductView : Window
    {
        public UpdateProductView()
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
