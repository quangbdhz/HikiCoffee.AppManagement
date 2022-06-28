using HikiCoffee.ApiIntegration.SuplierAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.Supliers;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.SuplierViewModels
{
    public class PageSuplierVM : BindableBase
    {
        private ObservableCollection<Suplier> _supliers;
        public ObservableCollection<Suplier> Supliers
        {
            get { return _supliers; }
            set { SetProperty(ref _supliers, value); }
        }

        private string _nameSuplier;
        public string NameSuplier
        {
            get { return _nameSuplier; }
            set { SetProperty(ref _nameSuplier, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _moreInfo;
        public string MoreInfo
        {
            get { return _moreInfo; }
            set { SetProperty(ref _moreInfo, value); }
        }

        private Suplier _selectedSuplier;
        public Suplier SelectedSuplier
        {
            get { return _selectedSuplier; }
            set 
            { 
                SetProperty(ref _selectedSuplier, value); 
                if(SelectedSuplier != null)
                {
                    NameSuplier = SelectedSuplier.NameSuplier;
                    Address = SelectedSuplier.Address;
                    Phone = SelectedSuplier.Phone;
                    Email = SelectedSuplier.Email;
                    MoreInfo = SelectedSuplier.MoreInfo;
                }
            }
        }

        public DelegateCommand AddSuplierCommand { get; set; }

        public DelegateCommand UpdateSuplierCommand { get; set; }

        public DelegateCommand DeleteSuplierCommand { get; set; }

        private readonly ISuplierAPI _suplierAPI;

        public PageSuplierVM()
        {
            _suplierAPI = new SuplierAPI();

            Loaded();

            AddSuplierCommand = new DelegateCommand(async () => 
            {
                SuplierCreateRequest request = new SuplierCreateRequest() { NameSuplier = NameSuplier, Address = Address, Email = Email, MoreInfo = MoreInfo, Phone = Phone };

                var result = await _suplierAPI.AddSuplier(request, SystemConstants.TokenInUse);
                if (!result.IsSuccessed)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                    messageDialogView.Show();
                }
                else
                {
                    Supliers = await _suplierAPI.GetAllSuplierManagements(SystemConstants.TokenInUse);

                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                    messageDialogView.Show();
                }
            }, () => 
            {
                if (string.IsNullOrEmpty(NameSuplier) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address))
                    return false;

                return true;
            }).ObservesProperty(() => NameSuplier).ObservesProperty(() => Email).ObservesProperty(() => Address);

            UpdateSuplierCommand = new DelegateCommand(async () => 
            {
                SuplierUpdateRequest request = new SuplierUpdateRequest() { Id = SelectedSuplier.Id, Address = Address, NameSuplier = NameSuplier, Email = Email, MoreInfo = MoreInfo, Phone = Phone };
                var result = await _suplierAPI.UpdateSuplier(request, SystemConstants.TokenInUse);

                if (!result.IsSuccessed)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                    messageDialogView.Show();
                }
                else
                {
                    Supliers = await _suplierAPI.GetAllSuplierManagements(SystemConstants.TokenInUse);

                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                    messageDialogView.Show();
                }
            }, () => 
            {
                if (string.IsNullOrEmpty(NameSuplier) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address) || SelectedSuplier == null)
                    return false;

                return true;
            }).ObservesProperty(() => NameSuplier).ObservesProperty(() => Email).ObservesProperty(() => Address).ObservesProperty(() => SelectedSuplier);


            DeleteSuplierCommand = new DelegateCommand(async () => 
            {
                var result = await _suplierAPI.DeleteSuplier(SelectedSuplier.Id, SystemConstants.TokenInUse);

                if (!result.IsSuccessed)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                    messageDialogView.Show();
                }
                else
                {
                    SelectedSuplier.IsActive = !SelectedSuplier.IsActive;

                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                    messageDialogView.Show();
                }
            }, () => 
            {
                if (SelectedSuplier == null)
                    return false;

                return true;
            }).ObservesProperty(() => SelectedSuplier);
        }

        private async void Loaded()
        {
            Supliers = new ObservableCollection<Suplier>();

            Supliers = await _suplierAPI.GetAllSuplierManagements(SystemConstants.TokenInUse);
        }
    }
}
