using System.Windows.Controls;

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
