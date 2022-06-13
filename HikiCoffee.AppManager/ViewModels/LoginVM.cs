using HikiCoffee.ApiIntegration.LanguageAPI;
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
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HikiCoffee.AppManager.ViewModels
{
    public class LoginVM : BindableBase
    {
        private ObservableCollection<Language> _languages;
        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            set { SetProperty(ref _languages, value); }
        }

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

        private int _languageIdDefault;
        public int LanguageIdDefault
        {
            get { return _languageIdDefault; }
            set { SetProperty(ref _languageIdDefault, value); }
        }

        public DelegateCommand<PasswordBox> PasswordChangedCommand { get; set; }
        public DelegateCommand<Window> GetStartedCommand { get; set; }
        public DelegateCommand<Button> MouseMoveButtonCloseCommand { get; set; }
        public DelegateCommand<Button> MouseLeaveButtonCloseCommand { get; set; }
        public DelegateCommand<Window> CloseWindowCommand { get; set; }
        public DelegateCommand<Window> CheckNetworkConnection { get; set; }
        public DelegateCommand<Language> SelectLanguageCommand { get; set; }
        
        private readonly TokenService tokenService;
        private readonly ILanguageAPI _languageAPI;


        public LoginVM()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            _languageAPI = new LanguageAPI();

            Languages = new ObservableCollection<Language>();

            ColorChangeButtonClose = "#2f3542";

            UserName = Rms.Read("UserInfo", "UserName", "");
            Password = Rms.Read("UserInfo", "Password", "");
            RememberMe = Rms.Read("UserInfo", "RememberMe", "") == "true" ? true : false;

            string languageId = Rms.Read("Language", "Id", "");
            if(languageId == "")
            {
                languageId = "0";
                Rms.Write("Language", "Id", "0");
            }


            Loaded();


            LanguageIdDefault = Int32.Parse(languageId);

            SystemConstants.LanguageIdInUse = LanguageIdDefault;

            PasswordChangedCommand = new DelegateCommand<PasswordBox>(ExecutePasswordChangedCommand).ObservesProperty(() => Password);

            MouseMoveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#EA2027"; }).ObservesProperty(() => ColorChangeButtonClose);

            MouseLeaveButtonCloseCommand = new DelegateCommand<Button>((p) => { ColorChangeButtonClose = "#2f3542"; }).ObservesProperty(() => ColorChangeButtonClose);

            CloseWindowCommand = new DelegateCommand<Window>((p) => { p.Close(); });

            GetStartedCommand = new DelegateCommand<Window>(ExecuteGetStartedCommand, CanExecuteGetStartedCommand).ObservesProperty(() => UserName).ObservesProperty(() => Password);

            CheckNetworkConnection = new DelegateCommand<Window>(ExecuteCheckNetworkConnection);

            SelectLanguageCommand = new DelegateCommand<Language>((p) =>
            {
                SystemConstants.LanguageIdInUse = p.Id;

                Rms.Write("Language", "Id", p.Id.ToString());
            });
        }

        private async void Loaded()
        {
            try
            {
                Languages = await _languageAPI.GetAllLanguages(null);

                int index = 0;
                foreach(var item in Languages)
                {
                    if(item.Id == LanguageIdDefault)
                    {
                        LanguageIdDefault = index;
                    }
                    index++;
                }
            }
            catch(Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
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
            LoginRequest loginRequest = new LoginRequest() { UserName = UserName, Password = Password, RememberMe = RememberMe };

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
                            SystemConstants.TokenInUse = myDeserializedObjList.Message;
                            SystemConstants.UserIdInUse = myDeserializedObjList.ResultObj;

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
