using HikiCoffee.ApiIntegration.UserAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HikiCoffee.AppManager.ViewModels.UserViewModels
{
    public class PageCustomerVM : BindableBase
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


        public DelegateCommand<User> MouseUpLvUserCommand { get; set; }

        public DelegateCommand DeleteUserCommand { get; set; }

        private readonly IUserAPI _userAPI;

        public PageCustomerVM()
        {
            _userAPI = new UserAPI();

            Loaded();

            MouseUpLvUserCommand = new DelegateCommand<User>((p) => { SelectedUser = p; });

            DeleteUserCommand = new DelegateCommand(async () => 
            {
                if(SelectedUser != null)
                {
                    var reponse = await _userAPI.DeleteUser(SelectedUser.Id, SystemConstants.TokenInUse);

                    var user = Users.Where(x => x.Id == SelectedUser.Id).FirstOrDefault();

                    if (user != null)
                    {
                        user.IsActive = !user.IsActive;
                    }

                    MessageDialogView messageDialogView = new MessageDialogView(reponse, 0);
                    messageDialogView.Show();

                    SelectedUser = null;
                }
            }, () =>
            {
                if (SelectedUser == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedUser);

        }

        private async void Loaded()
        {
            Users = new ObservableCollection<User>();   

            try
            {
                var reponse = await _userAPI.GetAllUsers(1, 10, SystemConstants.TokenInUse);

                Users = reponse.Items;

            }
            catch(Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }
    }
}
