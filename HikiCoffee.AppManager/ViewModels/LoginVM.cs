using HikiCoffee.ApiIntegration.CategoryApi;
using HikiCoffee.AppManager.DataRequests;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Views;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HikiCoffee.AppManager.ViewModels
{
    public class LoginVM : BindableBase
    {

        private string? _userName;
        public string? UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        private string? _colorChangeButtonClose;
        public string? ColorChangeButtonClose
        {
            get { return _colorChangeButtonClose; }
            set { SetProperty(ref _colorChangeButtonClose, value); }
        }

        public DelegateCommand<PasswordBox> PasswordChangedCommand { get; set; }
        public DelegateCommand<Window> GetStartedCommand { get; set; }
        public DelegateCommand<Button> MouseMoveButtonCloseCommand { get; set; }
        public DelegateCommand<Button> MouseLeaveButtonCloseCommand { get; set; }
        public DelegateCommand<Window> CloseWindowCommand { get; set; }
        public DelegateCommand<Window> CheckNetworkConnection { get; set; }

        private readonly ICategoryApi _categoryApi;
        private readonly TokenService tokenService;


        public LoginVM()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            _categoryApi = new CategoryApi();

            ColorChangeButtonClose = "#2f3542";

            UserName = Rms.Read("UserInfo", "UserName", "");
            Password = Rms.Read("UserInfo", "Password", "");
            RememberMe = Rms.Read("UserInfo", "RememberMe", "") == "true" ? true : false;

            PasswordChangedCommand = new DelegateCommand<PasswordBox>(ExecutePasswordChangedCommand).ObservesProperty(() => Password);

            MouseMoveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#EA2027"; }).ObservesProperty(() => ColorChangeButtonClose);

            MouseLeaveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#2f3542"; }).ObservesProperty(() => ColorChangeButtonClose);

            CloseWindowCommand = new DelegateCommand<Window>((p) => { p.Close(); });

            GetStartedCommand = new DelegateCommand<Window>(ExecuteGetStartedCommand, CanExecuteGetStartedCommand).ObservesProperty(() => UserName).ObservesProperty(() => Password);

            CheckNetworkConnection = new DelegateCommand<Window>(ExecuteCheckNetworkConnection);
        }

        private async void ExecuteCheckNetworkConnection(Window obj)
        {
            obj.Hide();
            bool checkNetworkConnection = await SystemConstants.CheckNetwork();

            if (!checkNetworkConnection)
            {
                MessageDialogView messageDialogView = new MessageDialogView("Internet Not Available", 1);
                messageDialogView.ShowDialog();
                obj.Close();
            }
            else
            {
                obj.Show();
            }
        }

        private bool CanExecuteGetStartedCommand(Window arg)
        {
            if (string.IsNullOrEmpty(Password))
                return false;

            if (string.IsNullOrEmpty(UserName))
                return false;

            return true;
        }

        private async void ExecuteGetStartedCommand(Window obj)
        {
            LoginRequest loginRequest = new LoginRequest() { UserName = UserName, Password = Password, RememberMe = RememberMe};

            var json = JsonConvert.SerializeObject(loginRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = SystemConstants.DomainName + "/api/Users/Login";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(url, data);

                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        ApiResult<Guid> myDeserializedObjList = (ApiResult<Guid>)JsonConvert.DeserializeObject(body, typeof(ApiResult<Guid>));

                        if (myDeserializedObjList != null)
                        {
                            tokenService.SaveToken(myDeserializedObjList.Message);

                            obj.Hide();

                            MainView mainView = new MainView();
                            mainView.ShowDialog();

                            obj.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(body, 1);
                        messageDialogView.Show();
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1); 
                    messageDialogView.Show();
                }
            }

        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            Password = obj.Password;
        }
    }
}
