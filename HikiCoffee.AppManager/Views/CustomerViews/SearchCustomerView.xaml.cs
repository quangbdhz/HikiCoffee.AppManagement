using HikiCoffee.ApiIntegration.UserAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HikiCoffee.AppManager.Views.CustomerViews
{
    /// <summary>
    /// Interaction logic for SearchCustomerView.xaml
    /// </summary>
    public partial class SearchCustomerView : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users { get => _users; set { _users = value; OnPropertyChanged(); } }

        private string _optionSearchCustomerOrder;
        public string OptionSearchCustomerOrder { get => _optionSearchCustomerOrder; set { _optionSearchCustomerOrder = value; OnPropertyChanged(); } }

        private readonly IUserAPI _userAPI;

        public SearchCustomerView()
        {
            InitializeComponent();

            _userAPI = new UserAPI();

            Loaded();
        }

        private new async void Loaded()
        {
            Users = new ObservableCollection<User>();
            OptionSearchCustomerOrder = "Last Name";


            try
            {
                var reponse = await _userAPI.GetAllUsers(1, 10, SystemConstants.TokenInUse);

                Users = reponse.Items;
                Lv_Users.ItemsSource = Users;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Users.ItemsSource);
                view.Filter = UserFilter;
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(tb_name_customer.Text))
                return true;
            else
            {
                if (OptionSearchCustomerOrder == "Last Name")
                {
                    return ((item as User).LastName.IndexOf(tb_name_customer.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (OptionSearchCustomerOrder == "First Name")
                {
                    return ((item as User).FirstName.IndexOf(tb_name_customer.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (OptionSearchCustomerOrder == "Phone")
                {
                    try
                    {
                        return ((item as User).PhoneNumber.IndexOf(tb_name_customer.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    catch//NullReferenceException ex
                    {
                        return true;
                    }
                }
                else if (OptionSearchCustomerOrder == "Mail")
                {
                    return ((item as User).Email.IndexOf(tb_name_customer.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Lv_Users.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(Lv_Users.ItemsSource).Refresh();
            }
        }

        private void OptionSearch_DropDownClosed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbx_number_item_order.Text))
            {
                OptionSearchCustomerOrder = cbx_number_item_order.Text;

                if (Lv_Users.ItemsSource != null)
                {
                    CollectionViewSource.GetDefaultView(Lv_Users.ItemsSource).Refresh();
                }
            }
        }

        private void LvUserScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
