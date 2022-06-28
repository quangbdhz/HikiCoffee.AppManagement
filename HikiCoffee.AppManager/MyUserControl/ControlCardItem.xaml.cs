using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HikiCoffee.AppManager.MyUserControl
{
    /// <summary>
    /// Interaction logic for ControlCardItem.xaml
    /// </summary>
    public partial class ControlCardItem : UserControl
    {
        public ControlCardItem()
        {
            InitializeComponent();
        }

        public string UrlImage
        {
            get { return (string)GetValue(UrlImageProperty); }
            set { SetValue(UrlImageProperty, value); }
        }

        public static readonly DependencyProperty UrlImageProperty =
           DependencyProperty.Register("UrlImage", typeof(string), typeof(ControlCardItem));

        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
           DependencyProperty.Register("Number", typeof(string), typeof(ControlCardItem));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
           DependencyProperty.Register("Description", typeof(string), typeof(ControlCardItem));

    }
}
