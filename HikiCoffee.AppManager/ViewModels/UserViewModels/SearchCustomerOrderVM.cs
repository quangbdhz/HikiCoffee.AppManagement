using HikiCoffee.ApiIntegration.UserAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HikiCoffee.AppManager.ViewModels.UserViewModels
{
    public class SearchCustomerOrderVM : BindableBase
    {

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private User? _selectedUser;
        public User? SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        private string _nameCustomerOrder;
        public string NameCustomerOrder
        {
            get { return _nameCustomerOrder; }
            set { SetProperty(ref _nameCustomerOrder, value); }
        }

        public DelegateCommand<User> MouseLvUserCommand { get; set; }

        public DelegateCommand<Window> MouseLvUserCloseWindowSearchUserCommand { get; set; }

        private readonly IUserAPI _userAPI;

        public SearchCustomerOrderVM(string nameCustomerOrder)
        {
            _userAPI = new UserAPI();

            NameCustomerOrder = nameCustomerOrder;

            Loaded();

            MouseLvUserCommand = new DelegateCommand<User>((p) =>
            {
                if (p != null)
                    SystemConstants.GetUserOrder = p;
            });

            MouseLvUserCloseWindowSearchUserCommand = new DelegateCommand<Window>((p) => { p.Close(); });
        }

        private async void Loaded()
        {
            Users = new ObservableCollection<User>();
            
            try
            {
                var reponse = await _userAPI.GetAllUsers(1, 10, SystemConstants.TokenInUse);

                Users = reponse.Items;

            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }
    }
}
