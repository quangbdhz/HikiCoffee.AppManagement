using System.Windows;
using System.Windows.Controls;

namespace HikiCoffee.AppManager.Views.CoffeeTableViews
{
    /// <summary>
    /// Interaction logic for MoveCoffeeTableView.xaml
    /// </summary>
    public partial class MoveCoffeeTableView : Window
    {
        public MoveCoffeeTableView()
        {
            InitializeComponent();
        }

        private void LvTableFurnitureScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
