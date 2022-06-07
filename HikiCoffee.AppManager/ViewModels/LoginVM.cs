using HikiCoffee.AppManager.DataRequests;
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
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string? _colorChangeButtonClose;
        public string? ColorChangeButtonClose
        {
            get { return _colorChangeButtonClose; }
            set { SetProperty(ref _colorChangeButtonClose, value); }
        }

        public DelegateCommand<PasswordBox> PasswordChangedCommand { get; set; }
        public DelegateCommand<Object> DirectionalWindow { get; set; }
        public DelegateCommand<Object> GetStartedCommand { get; set; }
        public DelegateCommand<Button> MouseMoveButtonCloseCommand { get; set; }
        public DelegateCommand<Button> MouseLeaveButtonCloseCommand { get; set; }
        public DelegateCommand<Window> CloseWindowCommand { get; set; }

        public LoginVM()
        {
            ColorChangeButtonClose = "#2f3542";

            PasswordChangedCommand = new DelegateCommand<PasswordBox>(ExecutePasswordChangedCommand).ObservesProperty(() => Password);

            MouseMoveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#EA2027"; }).ObservesProperty(() => ColorChangeButtonClose);

            MouseLeaveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#2f3542"; }).ObservesProperty(() => ColorChangeButtonClose);

            CloseWindowCommand = new DelegateCommand<Window>((p) => { p.Close(); });

            GetStartedCommand = new DelegateCommand<object>(ExecuteGetStartedCommand, CanExecuteGetStartedCommand).ObservesProperty(() => UserName).ObservesProperty(() => Password);
        }

        private bool CanExecuteGetStartedCommand(object arg)
        {
            if (string.IsNullOrEmpty(Password))
                return false;

            if (string.IsNullOrEmpty(UserName))
                return false;

            return true;
        }

        private async void ExecuteGetStartedCommand(object obj)
        {
            LoginRequest loginRequest = new LoginRequest() { UserName = UserName, Password = Password, RememberMe = true};

            var json = JsonConvert.SerializeObject(loginRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7227/api/Users/authenticate";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();

                string token = result;

                if (token != null && token != "False getAccount")
                {
                    Application.Current.Properties["token"] = token;
                }

                MessageBox.Show(token);
            }

        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            Password = obj.Password;
        }
    }
}
