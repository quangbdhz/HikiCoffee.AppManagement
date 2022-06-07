using System.Windows;
using System.Windows.Controls;

namespace HikiCoffee.AppManager.MyUserControl
{
    /// <summary>
    /// Interaction logic for ControlTableFurniture.xaml
    /// </summary>
    public partial class ControlTableFurniture : UserControl
    {


        public ControlTableFurniture()
        {
            InitializeComponent();
        }

        public string UrlImage
        {
            get { return (string)GetValue(UrlImageProperty); }
            set { SetValue(UrlImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlImageProperty =
            DependencyProperty.Register("UrlImage", typeof(string), typeof(ControlTableFurniture));

        public string NameTable
        {
            get { return (string)GetValue(NameTableProperty); }
            set { SetValue(NameTableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameTableProperty =
            DependencyProperty.Register("NameTable", typeof(string), typeof(ControlTableFurniture));

        public string BackgroundTable
        {
            get { return (string)GetValue(BackgroundTableProperty); }
            set { SetValue(BackgroundTableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundTableProperty =
            DependencyProperty.Register("BackgroundTable", typeof(string), typeof(ControlTableFurniture));
    }
}
