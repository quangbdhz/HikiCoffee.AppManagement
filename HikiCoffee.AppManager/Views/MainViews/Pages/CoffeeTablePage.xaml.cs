using System.Windows.Controls;

namespace HikiCoffee.AppManager.Views.MainViews.Pages
{
    /// <summary>
    /// Interaction logic for TableFurniturePage.xaml
    /// </summary>
    public partial class CoffeeTablePage : Page
    {
        public CoffeeTablePage()
        {
            InitializeComponent();
        }

        private void LvCustomerOderItemScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void LvProductItemOderScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void LvTableFurnitureScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

    }
}
