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
using System.Windows.Shapes;

namespace HikiCoffee.AppManager.Views.CoffeeTableViews
{
    /// <summary>
    /// Interaction logic for MergeCoffeeTableView.xaml
    /// </summary>
    public partial class MergeCoffeeTableView : Window
    {
        public MergeCoffeeTableView()
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
