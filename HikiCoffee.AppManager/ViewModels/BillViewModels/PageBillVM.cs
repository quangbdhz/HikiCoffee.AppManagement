using HikiCoffee.ApiIntegration.BillAPI;
using HikiCoffee.ApiIntegration.BillInfoAPI;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.BillViewModels
{
    public class PageBillVM : BindableBase
    {
        #region bill
        private ObservableCollection<Bill> _bills;
        public ObservableCollection<Bill> Bills
        {
            get { return _bills; }
            set { SetProperty(ref _bills, value); }
        }

        private Bill? _selectedBill;
        public Bill? SelectedBill
        {
            get { return _selectedBill; }
            set { SetProperty(ref _selectedBill, value); }
        }

        private string _infoBillChoosed;
        public string InfoBillChoosed
        {
            get { return _infoBillChoosed; }
            set { SetProperty(ref _infoBillChoosed, value); }
        }

        public DelegateCommand<Bill> GetSelectedBillCommand { get; set; }
        #endregion

        #region bill info
        private ObservableCollection<BillInfo> _billInfos;
        public ObservableCollection<BillInfo> BillInfos
        {
            get { return _billInfos; }
            set { SetProperty(ref _billInfos, value); }
        }


        #endregion

        private readonly IBillAPI _billAPI;

        private readonly IBillInfoAPI _billInfoAPI;

        public PageBillVM()
        {
            _billAPI = new BillAPI();
            _billInfoAPI = new BillInfoAPI();

            Loaded();

            GetSelectedBillCommand = new DelegateCommand<Bill>(async (p) => 
            { 
                if (p != null)
                {
                    SelectedBill = p;

                    InfoBillChoosed = $"Bill Info With Bill Id '" + p.BillId.ToString() + "'";

                    BillInfos = await _billInfoAPI.GetBillInfoByBillId(p.BillId, SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
                }
            });
        }

        private async void Loaded()
        {
            Bills = new ObservableCollection<Bill>();
            BillInfos = new ObservableCollection<BillInfo>();

            InfoBillChoosed = "Bill Info";

            var pageResultBills = await _billAPI.GetAllBill(1, 20, SystemConstants.TokenInUse);

            Bills = pageResultBills.Items;
        }
    }
}
