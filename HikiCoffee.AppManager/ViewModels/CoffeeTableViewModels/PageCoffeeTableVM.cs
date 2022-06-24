using HikiCoffee.ApiIntegration.BillAPI;
using HikiCoffee.ApiIntegration.BillInfoAPI;
using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.ApiIntegration.CoffeeTableAPI;
using HikiCoffee.ApiIntegration.ProductTranslationAPI;
using HikiCoffee.AppManager.ViewModels.UserViewModels;
using HikiCoffee.AppManager.Views.CoffeeTableViews;
using HikiCoffee.AppManager.Views.CustomerViews;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.BillInfos;
using HikiCoffee.Models.DataRequest.Bills;
using HikiCoffee.Models.DataRequest.CoffeeTables;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HikiCoffee.AppManager.ViewModels.CoffeeTableViewModels
{
    public class PageCoffeeTableVM : BindableBase
    {
        private ObservableCollection<CategoryTranslationWithUrl> _categoryTranslations;
        public ObservableCollection<CategoryTranslationWithUrl> CategoryTranslations
        {
            get { return _categoryTranslations; }
            set { SetProperty(ref _categoryTranslations, value); }
        }

        private ObservableCollection<ItemOrder> _itemOrders;
        public ObservableCollection<ItemOrder> ItemOrders
        {
            get { return _itemOrders; }
            set { SetProperty(ref _itemOrders, value); }
        }

        private ObservableCollection<CoffeeTable> _coffeeTables;
        public ObservableCollection<CoffeeTable> CoffeeTables
        {
            get { return _coffeeTables; }
            set { SetProperty(ref _coffeeTables, value); }
        }

        private ObservableCollection<int> _numberItemOrders;
        public ObservableCollection<int> NumberItemOrders
        {
            get { return _numberItemOrders; }
            set { SetProperty(ref _numberItemOrders, value); }
        }

        private ObservableCollection<BillInfo> _billInfoCustomerOrders;
        public ObservableCollection<BillInfo> BillInfoCustomerOrders
        {
            get { return _billInfoCustomerOrders; }
            set { SetProperty(ref _billInfoCustomerOrders, value); }
        }

        private CategoryTranslation _selectedCategoryTranslation;
        public CategoryTranslation SelectedCategoryTranslation
        {
            get { return _selectedCategoryTranslation; }
            set { SetProperty(ref _selectedCategoryTranslation, value); }
        }



        #region settings
        private CoffeeTable? _coffeeTableChoosed;
        public CoffeeTable? CoffeeTableChoosed
        {
            get { return _coffeeTableChoosed; }
            set { SetProperty(ref _coffeeTableChoosed, value); }
        }

        private Bill? _billCoffeeTable;
        public Bill? BillCoffeeTable
        {
            get { return _billCoffeeTable; }
            set { SetProperty(ref _billCoffeeTable, value); }
        }

        private string _nameTableChoosed;
        public string NameTableChoosed
        {
            get { return _nameTableChoosed; }
            set { SetProperty(ref _nameTableChoosed, value); }
        }

        private Visibility _visibilityButtonSelectedCoffeeTable;
        public Visibility VisibilityButtonSelectedCoffeeTable
        {
            get { return _visibilityButtonSelectedCoffeeTable; }
            set { SetProperty(ref _visibilityButtonSelectedCoffeeTable, value); }
        }

        private string _nameCategoryChoose;
        public string NameCategoryChoosed
        {
            get { return _nameCategoryChoose; }
            set { SetProperty(ref _nameCategoryChoose, value); }
        }

        #endregion

        #region property coffeetable change
        private static CoffeeTable? _staticGetCoffeeTableChange;
        public static CoffeeTable? StaticGetCoffeeTableChange { get => _staticGetCoffeeTableChange; set { _staticGetCoffeeTableChange = value; } }

        private static CoffeeTable? _staticGetCoffeeTableMoveChange;
        public static CoffeeTable? StaticGetCoffeeTableMoveChange { get => _staticGetCoffeeTableMoveChange; set { _staticGetCoffeeTableMoveChange = value; } }

        private static CoffeeTable? _staticGetCoffeeTableMergeChange;
        public static CoffeeTable? StaticGetCoffeeTableMergeChange { get => _staticGetCoffeeTableMergeChange; set { _staticGetCoffeeTableMergeChange = value; } }
        #endregion

        #region customer
        private User? _infoOfCustomerOrder;
        public User? InfoOfCustomerOrder
        {
            get { return _infoOfCustomerOrder; }
            set { SetProperty(ref _infoOfCustomerOrder, value); }
        }

        private BillInfo? _getBillInfoInsideBill;
        public BillInfo? GetBillInfoInsideBill
        {
            get { return _getBillInfoInsideBill; }
            set { SetProperty(ref _getBillInfoInsideBill, value); }
        }

        private string _fullNameCustomerOrder;
        public string FullNameCustomerOrder
        {
            get { return _fullNameCustomerOrder; }
            set { SetProperty(ref _fullNameCustomerOrder, value); }
        }

        private string _searchNameCustomerOrder;
        public string SearchNameCustomerOrder
        {
            get { return _searchNameCustomerOrder; }
            set { SetProperty(ref _searchNameCustomerOrder, value); }
        }

        private decimal _totalDueBillOfCustomerOrder;
        public decimal TotalDueBillOfCustomerOrder
        {
            get { return _totalDueBillOfCustomerOrder; }
            set { SetProperty(ref _totalDueBillOfCustomerOrder, value); }
        }

        private int _indexCountProductOrderInBill;
        public int IndexCountProductOrderInBill
        {
            get { return _indexCountProductOrderInBill; }
            set { SetProperty(ref _indexCountProductOrderInBill, value); }
        }

        #endregion

        public DelegateCommand<CategoryTranslation> GetListItemByCategoryCommand { get; set; }

        public DelegateCommand<ItemOrder> GetItemCustomerOderCommand { get; set; }

        public DelegateCommand<CoffeeTable> GetSelectedCoffeeTableCommand{ get; set; }

        public DelegateCommand<BillInfo> GetSelectedBillInfoInsideBillCommand { get; set; }

        public DelegateCommand SearchCustomerOrderCommand { get; set; }

        public DelegateCommand TurnOnCoffeeTableCommand { get; set; }

        public DelegateCommand<ComboBox> GetIndexCountProductOrderInBillCommand { get; set; }

        #region control coffee table
        public DelegateCommand MoveCoffeeTableCommand { get; set; }

        public DelegateCommand MergeCoffeeTableComamnd { get; set; }
        #endregion

        #region control quantity billinfo
        public DelegateCommand AddQuantityBillInfoCoffeeTableCommand { get; set; }

        public DelegateCommand MinusQuantityBillInfoCoffeeTableCommand { get; set; }

        public DelegateCommand DeleteQuantityBillInfoCoffeeTableCommand { get; set; }

        public DelegateCommand CheckOutBillCoffeeTableCommand { get; set; }
        #endregion

        #region checkout bill
        private decimal _checkOutPercentVAT;
        public decimal CheckOutPercentVAT
        {
            get { return _checkOutPercentVAT; }
            set { SetProperty(ref _checkOutPercentVAT, value); }
        }

        private decimal _percentVAT;
        public decimal PercentVAT
        {
            get { return _percentVAT; }
            set { SetProperty(ref _percentVAT, value); }
        }

        private decimal _toTalPayment;
        public decimal ToTalPayment
        {
            get { return _toTalPayment; }
            set { SetProperty(ref _toTalPayment, value); }
        }

        private string _promotionalCode;
        public string PromotionalCode
        {
            get { return _promotionalCode; }
            set { SetProperty(ref _promotionalCode, value); }
        }

        private decimal _valuePromotional;
        public decimal ValuePromotional
        {
            get { return _valuePromotional; }
            set { SetProperty(ref _valuePromotional, value); }
        }

        public DelegateCommand<ComboBox> SelectedVATCommand { get; set; }
        #endregion

        #region interface
        private readonly ICoffeeTableAPI _coffeeTableAPI;

        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        private readonly IProductTranslationAPI _productTranslationAPI;

        private readonly IBillAPI _billAPI;

        private readonly IBillInfoAPI _billInfoAPI;
        #endregion

        public PageCoffeeTableVM()
        {
            _categoryTranslationAPI = new CategoryTranslationAPI();
            _productTranslationAPI = new ProductTranslationAPI();
            _coffeeTableAPI = new CoffeeTableAPI();
            _billAPI = new BillAPI();
            _billInfoAPI = new BillInfoAPI();

            Loaded();

            GetListItemByCategoryCommand = new DelegateCommand<CategoryTranslation>(async (p) =>
            {
                if (p != null)
                {
                    SelectedCategoryTranslation = p;

                    NameCategoryChoosed = "Category: '" + p.NameCategory + "'";

                    ItemOrders = await _productTranslationAPI.GetAllProductTranslationByCategoryId(SelectedCategoryTranslation.CategoryId, SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
                }

            });

            GetSelectedCoffeeTableCommand = new DelegateCommand<CoffeeTable>(async (p) =>
            {
                if (p != null)
                {
                    InfoOfCustomerOrder = null;
                    GetBillInfoInsideBill = null;
                    NameTableChoosed = p.NameCoffeeTable;
                    CoffeeTableChoosed = p;
                    TotalDueBillOfCustomerOrder = 0;
                    CheckOutPercentVAT = 0;
                    ToTalPayment = 0;
                    IndexCountProductOrderInBill = 0;
                    BillInfoCustomerOrders.Clear();

                    if (p.StatusId == 3)
                    {
                        VisibilityButtonSelectedCoffeeTable = Visibility.Visible;
                        BillCoffeeTable = null;
                        FullNameCustomerOrder = "";
                    }
                    else
                    {
                        Bill result = await _billAPI.GetBillIdOfCoffeeTable(CoffeeTableChoosed.Id, SystemConstants.TokenInUse);
                        BillCoffeeTable = result;
                        FullNameCustomerOrder = result.NameCustomer;

                        if (result.BillId != 0)
                        {
                            BillInfoCustomerOrders = await _billInfoAPI.GetAllBillInfoCustomerOrder(result.BillId, SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);

                            foreach (var item in BillInfoCustomerOrders)
                            {
                                TotalDueBillOfCustomerOrder += item.Amount;
                            }
                        }

                        UpdateCheckOutPercentVAT();
                        VisibilityButtonSelectedCoffeeTable = Visibility.Hidden;
                    }
                }
            }).ObservesProperty(() => NameTableChoosed).ObservesProperty(() => VisibilityButtonSelectedCoffeeTable).ObservesProperty(() => CoffeeTableChoosed);

            SearchCustomerOrderCommand = new DelegateCommand(() =>
            {
                SearchCustomerView searchCustomerView = new SearchCustomerView();
                searchCustomerView.DataContext = new SearchCustomerOrderVM(SearchNameCustomerOrder);
                searchCustomerView.ShowDialog();

                if (SystemConstants.GetUserOrder != null)
                {
                    InfoOfCustomerOrder = SystemConstants.GetUserOrder;
                    FullNameCustomerOrder = InfoOfCustomerOrder.LastName + " " + InfoOfCustomerOrder.FirstName;
                }
            }, () =>
            {
                if (string.IsNullOrEmpty(SearchNameCustomerOrder))
                    return false;
                return true;
            }).ObservesProperty(() => SearchNameCustomerOrder);

            TurnOnCoffeeTableCommand = new DelegateCommand(async () =>
            {
                if (CoffeeTableChoosed != null && InfoOfCustomerOrder != null)
                {
                    ChangeStatusCoffeeTableRequest request = new ChangeStatusCoffeeTableRequest() { CoffeeTableId = CoffeeTableChoosed.Id, StatusId = 4 };

                    ApiResult<bool> result = await _coffeeTableAPI.ChangeStatusCoffeeTable(request, SystemConstants.TokenInUse);

                    if (result.IsSuccessed)
                    {
                        var coffeeTable = CoffeeTables.Where(x => x.Id == CoffeeTableChoosed.Id).FirstOrDefault();
                        if (coffeeTable != null)
                        {
                            coffeeTable.StatusId = 4;
                        }

                        BillCreateRequest billCreateRequest = new BillCreateRequest() { CoffeeTableId = CoffeeTableChoosed.Id, UserCustomerId = InfoOfCustomerOrder.Id, UserPaymentId = SystemConstants.UserIdInUse };

                        var reponseBillCreateRequest = await _billAPI.AddBill(billCreateRequest, SystemConstants.TokenInUse);

                        if (!reponseBillCreateRequest.IsSuccessed)
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(reponseBillCreateRequest.Message, 1);
                            messageDialogView.Show();
                        }

                        BillCoffeeTable = new Bill() { BillId = reponseBillCreateRequest.ResultObj };
                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }

                    VisibilityButtonSelectedCoffeeTable = Visibility.Hidden;
                }
            }, () =>
             {
                 if (InfoOfCustomerOrder == null)
                     return false;

                 return true;
             }).ObservesProperty(() => CoffeeTableChoosed).ObservesProperty(() => VisibilityButtonSelectedCoffeeTable).ObservesProperty(() => InfoOfCustomerOrder);

            GetItemCustomerOderCommand = new DelegateCommand<ItemOrder>(async (p) =>
            {
                if (BillCoffeeTable != null && p != null)
                {
                    TotalDueBillOfCustomerOrder += p.Price;

                    var checkBillInfoExits = BillInfoCustomerOrders.SingleOrDefault(x => x.ProductId == p.ProductId);
                    if (checkBillInfoExits == null)
                    {
                        BillInfo billInfo = new BillInfo() { NameProduct = p.NameProduct, ProductId = p.ProductId, Amount = p.Price, Price = p.Price, Quantity = 1, BillId = BillCoffeeTable.BillId };
                        BillInfoCustomerOrders.Add(billInfo);

                        BillInfoCreateRequest request = new BillInfoCreateRequest() { BillId = BillCoffeeTable.BillId, Amount = p.Price, Price = p.Price, Quantity = 1, ProductId = p.ProductId };
                        var reponse = await _billInfoAPI.AddBillInfo(request, SystemConstants.TokenInUse);

                        if (!reponse.IsSuccessed)
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(reponse.Message, 1);
                            messageDialogView.Show();
                        }
                        else
                        {
                            UpdateCheckOutPercentVAT();
                        }
                    }
                }
            }).ObservesProperty(() => BillInfoCustomerOrders);

            GetSelectedBillInfoInsideBillCommand = new DelegateCommand<BillInfo>((p) => { if (p != null) { GetBillInfoInsideBill = p; IndexCountProductOrderInBill = p.Quantity; } });

            GetIndexCountProductOrderInBillCommand = new DelegateCommand<ComboBox>(async (p) =>
            {
                if (p != null)
                {
                    if (GetBillInfoInsideBill != null && IndexCountProductOrderInBill != 0)
                    {
                        int quantityCurrent = GetBillInfoInsideBill.Quantity;
                        decimal totalDueCurrent = TotalDueBillOfCustomerOrder;

                        TotalDueBillOfCustomerOrder -= GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;
                        TotalDueBillOfCustomerOrder += GetBillInfoInsideBill.Price * IndexCountProductOrderInBill;

                        GetBillInfoInsideBill.Quantity = IndexCountProductOrderInBill;
                        GetBillInfoInsideBill.Amount = GetBillInfoInsideBill.Price * IndexCountProductOrderInBill;

                        BillInfoUpdateRequest request = new BillInfoUpdateRequest()
                        {
                            BillId = GetBillInfoInsideBill.BillId,
                            ProductId = GetBillInfoInsideBill.ProductId,
                            Amount = GetBillInfoInsideBill.Amount,
                            Price = GetBillInfoInsideBill.Price,
                            Quantity = GetBillInfoInsideBill.Quantity
                        };

                        var result = await _billInfoAPI.UpdateBillInfo(request, SystemConstants.TokenInUse);

                        if (!result.IsSuccessed)
                        {
                            if (result.Message.IndexOf("Stock Product equals ") != -1)
                            {
                                GetBillInfoInsideBill.Quantity = quantityCurrent;
                                GetBillInfoInsideBill.Amount = quantityCurrent * GetBillInfoInsideBill.Price;
                            }

                            MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                            messageDialogView.Show();
                        }
                        else
                        {
                            UpdateCheckOutPercentVAT();
                        }
                    }
                }
            }, (p) =>
            {
                if (GetBillInfoInsideBill == null)
                    return false;
                return true;
            }).ObservesProperty(() => GetBillInfoInsideBill);

            #region control coffee table
            MoveCoffeeTableCommand = new DelegateCommand(async () =>
            {
                if (CoffeeTableChoosed != null && BillCoffeeTable != null)
                {
                    StaticGetCoffeeTableChange = CoffeeTableChoosed;

                    MoveCoffeeTableView moveCoffeeTableView = new MoveCoffeeTableView();
                    moveCoffeeTableView.ShowDialog();

                    if (StaticGetCoffeeTableMoveChange != null)
                    {
                        ChangeCoffeeTableIdInBillRequest request = new ChangeCoffeeTableIdInBillRequest() { BillId = BillCoffeeTable.BillId, NewCoffeeTableId = StaticGetCoffeeTableMoveChange.Id };

                        var result = await _coffeeTableAPI.ChangeCoffeeTableIdInBill(request, SystemConstants.TokenInUse);

                        if (!result.IsSuccessed)
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                            messageDialogView.Show();
                        }
                        else
                        {
                            CoffeeTableChoosed.StatusId = 3;

                            CoffeeTableChoosed = CoffeeTables.SingleOrDefault(x => x.Id == StaticGetCoffeeTableMoveChange.Id);

                            if (CoffeeTableChoosed != null)
                            {
                                NameTableChoosed = CoffeeTableChoosed.NameCoffeeTable;
                                CoffeeTableChoosed.StatusId = 4;
                            }
                        }
                    }

                    StaticGetCoffeeTableMoveChange = null;
                    StaticGetCoffeeTableChange = null;
                }
            }, () =>
            {
                if (CoffeeTableChoosed == null || CoffeeTableChoosed.StatusId == 3)
                    return false;
                return true;
            }).ObservesProperty(() => CoffeeTableChoosed);

            MergeCoffeeTableComamnd = new DelegateCommand(async () =>
            {
                if (CoffeeTableChoosed != null && BillCoffeeTable != null)
                {
                    StaticGetCoffeeTableChange = CoffeeTableChoosed;

                    var copyOldBillInfoCustomerOrders = BillInfoCustomerOrders;
                    var copyOldBillCoffeeTable = BillCoffeeTable;

                    MergeCoffeeTableView mergeCoffeeTableView = new MergeCoffeeTableView();
                    mergeCoffeeTableView.ShowDialog();

                    if (StaticGetCoffeeTableMergeChange != null)
                    {

                        Bill result = await _billAPI.GetBillIdOfCoffeeTable(StaticGetCoffeeTableMergeChange.Id, SystemConstants.TokenInUse);
                        BillCoffeeTable = result;
                        FullNameCustomerOrder = result.NameCustomer;

                        if (result.BillId != 0)
                        {
                            BillInfoCustomerOrders = await _billInfoAPI.GetAllBillInfoCustomerOrder(result.BillId, SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);

                            ObservableCollection<BillInfo> listUpdate = new ObservableCollection<BillInfo>();
                            ObservableCollection<BillInfo> listAdd = new ObservableCollection<BillInfo>();
                            ObservableCollection<BillInfo> updateBillInfo = new ObservableCollection<BillInfo>();

                            foreach (var item in copyOldBillInfoCustomerOrders)
                            {
                                var checkExist = CheckBillInfoIsExist(item, BillInfoCustomerOrders);

                                if (checkExist == false)
                                {
                                    listAdd.Add(item);
                                }
                                else
                                {
                                    listUpdate.Add(item);
                                }
                            }

                            //delete list old billinfo
                            foreach (var item in copyOldBillInfoCustomerOrders)
                            {
                                var reponseBillInfoDelete = await _billInfoAPI.DeleteBillInfo(item.BillId, item.ProductId, SystemConstants.TokenInUse);
                                if (!reponseBillInfoDelete.IsSuccessed)
                                {
                                    MessageDialogView messageDialogView = new MessageDialogView(reponseBillInfoDelete.Message, 1);
                                    messageDialogView.Show();
                                }
                            }

                            //new item in bill
                            foreach (var item in listAdd)
                            {
                                BillInfoCustomerOrders.Add(item);

                                BillInfoCreateRequest request = new BillInfoCreateRequest() { Amount = item.Amount, BillId = BillCoffeeTable.BillId, Price = item.Price, ProductId = item.ProductId, Quantity = item.Quantity };
                                var responseBillInfoCreate = await _billInfoAPI.AddBillInfo(request, SystemConstants.TokenInUse);
                                if (!responseBillInfoCreate.IsSuccessed)
                                {
                                    MessageDialogView messageDialogView = new MessageDialogView(responseBillInfoCreate.Message, 1);
                                    messageDialogView.Show();
                                }
                            }

                            //item update in bill
                            foreach (var item in listUpdate)
                            {
                                var billInfo = BillInfoCustomerOrders.FirstOrDefault(x => x.ProductId == item.ProductId);

                                if (billInfo != null)
                                {
                                    billInfo.Quantity += item.Quantity;
                                    billInfo.Amount = billInfo.Quantity * billInfo.Price;

                                    BillInfoUpdateRequest request = new BillInfoUpdateRequest() { BillId = billInfo.BillId, ProductId = billInfo.ProductId, Amount = billInfo.Amount, Price = billInfo.Price, Quantity = billInfo.Quantity };
                                    var reponseBillInfoUpdate = await _billInfoAPI.UpdateBillInfo(request, SystemConstants.TokenInUse);
                                    if (!reponseBillInfoUpdate.IsSuccessed)
                                    {
                                        MessageDialogView messageDialogView = new MessageDialogView(reponseBillInfoUpdate.Message, 1);
                                        messageDialogView.Show();
                                    }
                                }
                            }

                            ChangeStatusCoffeeTableRequest changeStatusCoffeeTableRequest = new ChangeStatusCoffeeTableRequest() { CoffeeTableId = CoffeeTableChoosed.Id, StatusId = 3 };
                            var responseChangeStatusCoffeeTable = await _coffeeTableAPI.ChangeStatusCoffeeTable(changeStatusCoffeeTableRequest, SystemConstants.TokenInUse);
                            if (!responseChangeStatusCoffeeTable.IsSuccessed)
                            {
                                MessageDialogView messageDialogView = new MessageDialogView(responseChangeStatusCoffeeTable.Message, 1);
                                messageDialogView.Show();
                            }

                            CoffeeTableChoosed.StatusId = 3;

                            CoffeeTableChoosed = CoffeeTables.SingleOrDefault(x => x.Id == StaticGetCoffeeTableMergeChange.Id);

                            if (CoffeeTableChoosed != null)
                            {
                                NameTableChoosed = CoffeeTableChoosed.NameCoffeeTable;
                            }

                            MergeBillRequest mergeBillRequest = new MergeBillRequest() { BillId = copyOldBillCoffeeTable.BillId, StatusId = 5 };
                            var responseMergeBill = await _billAPI.MergeBill(mergeBillRequest, SystemConstants.TokenInUse);
                            if (!responseMergeBill.IsSuccessed)
                            {
                                MessageDialogView messageDialogView = new MessageDialogView(responseMergeBill.Message, 1);
                                messageDialogView.Show();
                            }

                            TotalDueBillOfCustomerOrder = 0;
                            foreach (var item in BillInfoCustomerOrders)
                            {
                                TotalDueBillOfCustomerOrder += item.Amount;
                            }

                            UpdateCheckOutPercentVAT();
                        }
                    }
                }
            }, () =>
            {
                if (CoffeeTableChoosed == null || CoffeeTableChoosed.StatusId == 3)
                    return false;
                return true;
            }).ObservesProperty(() => CoffeeTableChoosed);
            #endregion

            #region control bill coffee table
            AddQuantityBillInfoCoffeeTableCommand = new DelegateCommand(async () =>
            {
                if (GetBillInfoInsideBill != null)
                {
                    TotalDueBillOfCustomerOrder += GetBillInfoInsideBill.Price;
                    GetBillInfoInsideBill.Quantity++;
                    GetBillInfoInsideBill.Amount = GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;

                    BillInfoUpdateRequest request = new BillInfoUpdateRequest() {
                        BillId = GetBillInfoInsideBill.BillId,
                        ProductId = GetBillInfoInsideBill.ProductId,
                        Amount = GetBillInfoInsideBill.Amount,
                        Price = GetBillInfoInsideBill.Price,
                        Quantity = GetBillInfoInsideBill.Quantity
                    };

                    var result = await _billInfoAPI.UpdateBillInfo(request, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        if (result.Message.IndexOf("Stock Product equals ") != -1)
                        {
                            GetBillInfoInsideBill.Quantity--;
                            GetBillInfoInsideBill.Amount = GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;
                            TotalDueBillOfCustomerOrder -= GetBillInfoInsideBill.Price;
                        }

                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        UpdateCheckOutPercentVAT();
                    }
                }
            }, () =>
            {
                if (GetBillInfoInsideBill == null)
                    return false;
                return true;
            }).ObservesProperty(() => GetBillInfoInsideBill).ObservesProperty(() => BillInfoCustomerOrders);

            MinusQuantityBillInfoCoffeeTableCommand = new DelegateCommand(async () =>
            {
                if (GetBillInfoInsideBill != null)
                {
                    if (GetBillInfoInsideBill.Quantity > 0)
                    {
                        TotalDueBillOfCustomerOrder -= GetBillInfoInsideBill.Price;
                        GetBillInfoInsideBill.Quantity--;
                        GetBillInfoInsideBill.Amount = GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;

                        BillInfoUpdateRequest request = new BillInfoUpdateRequest()
                        {
                            BillId = GetBillInfoInsideBill.BillId,
                            ProductId = GetBillInfoInsideBill.ProductId,
                            Amount = GetBillInfoInsideBill.Amount,
                            Price = GetBillInfoInsideBill.Price,
                            Quantity = GetBillInfoInsideBill.Quantity
                        };

                        var result = await _billInfoAPI.UpdateBillInfo(request, SystemConstants.TokenInUse);

                        if (GetBillInfoInsideBill.Quantity == 0)
                        {
                            var resultDelete = await _billInfoAPI.DeleteBillInfo(GetBillInfoInsideBill.BillId, GetBillInfoInsideBill.ProductId, SystemConstants.TokenInUse);

                            if (!resultDelete.IsSuccessed)
                            {
                                TotalDueBillOfCustomerOrder += GetBillInfoInsideBill.Price;
                                MessageDialogView messageDialogView = new MessageDialogView(resultDelete.Message, 1);
                                messageDialogView.Show();
                            }

                            BillInfoCustomerOrders.Remove(GetBillInfoInsideBill);
                            GetBillInfoInsideBill = null;
                        }
                        else
                        {
                            if (!result.IsSuccessed)
                            {
                                TotalDueBillOfCustomerOrder += GetBillInfoInsideBill.Price;
                                MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                                messageDialogView.Show();
                            }
                        }

                        UpdateCheckOutPercentVAT();
                    }
                }
            }, () =>
            {
                if (GetBillInfoInsideBill == null || GetBillInfoInsideBill.Quantity == 0)
                    return false;
                return true;
            }).ObservesProperty(() => GetBillInfoInsideBill);

            DeleteQuantityBillInfoCoffeeTableCommand = new DelegateCommand(async () =>
            {
                if (GetBillInfoInsideBill != null)
                {
                    TotalDueBillOfCustomerOrder = TotalDueBillOfCustomerOrder - GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;
                    var result = await _billInfoAPI.DeleteBillInfo(GetBillInfoInsideBill.BillId, GetBillInfoInsideBill.ProductId, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        TotalDueBillOfCustomerOrder += GetBillInfoInsideBill.Price * GetBillInfoInsideBill.Quantity;
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }

                    BillInfoCustomerOrders.Remove(GetBillInfoInsideBill);
                    GetBillInfoInsideBill = null;
                    UpdateCheckOutPercentVAT();
                }
            }, () =>
            {
                if (GetBillInfoInsideBill == null)
                    return false;
                return true;
            }).ObservesProperty(() => GetBillInfoInsideBill);
            #endregion

            #region checkout bill
            SelectedVATCommand = new DelegateCommand<ComboBox>((p) => 
            {
                string value = p.Text;

                PercentVAT = Int32.Parse(value);
                UpdateCheckOutPercentVAT();
            });

            CheckOutBillCoffeeTableCommand = new DelegateCommand( async () => 
            {
                if(BillCoffeeTable != null && CoffeeTableChoosed != null)
                {
                    BillCheckOutRequest request = new BillCheckOutRequest() { BillId = BillCoffeeTable.BillId, TotalPayPrice = TotalDueBillOfCustomerOrder };

                    var result = await _billAPI.BillCheckOut(request, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        PrintBill();

                        CoffeeTableChoosed.StatusId = 3;
                        TotalDueBillOfCustomerOrder = 0;
                        BillCoffeeTable = null;
                        BillInfoCustomerOrders.Clear();
                        CoffeeTableChoosed = null;
                        IndexCountProductOrderInBill = 0;
                        GetBillInfoInsideBill = null;

                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                        messageDialogView.Show();
                    }
                }
            }, () =>
            {
                if (BillCoffeeTable == null)
                    return false;
                return true;
            }).ObservesProperty(() => BillCoffeeTable);
            #endregion
        }

        private async void Loaded()
        {
            CategoryTranslations = new ObservableCollection<CategoryTranslationWithUrl>();
            ItemOrders = new ObservableCollection<ItemOrder>();
            CoffeeTables = new ObservableCollection<CoffeeTable>();
            NumberItemOrders = new ObservableCollection<int>() { 0, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 };
            BillInfoCustomerOrders = new ObservableCollection<BillInfo>();

            VisibilityButtonSelectedCoffeeTable = Visibility.Hidden;
            NameCategoryChoosed = "Category";
            TotalDueBillOfCustomerOrder = 0;
            PercentVAT = 10;
            CheckOutPercentVAT = 0;
            ValuePromotional = 0;

            CategoryTranslations = await _categoryTranslationAPI.GetAllCategoryTranslationWithUrlByLanguageId(SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
            CoffeeTables = await _coffeeTableAPI.GetAllCoffeeTable(SystemConstants.TokenInUse);
        }

        private bool CheckBillInfoIsExist(BillInfo value, ObservableCollection<BillInfo> billInfos)
        {
            foreach (var item in billInfos)
            {
                if (item.ProductId == value.ProductId)
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateCheckOutPercentVAT()
        {
            CheckOutPercentVAT = PercentVAT * TotalDueBillOfCustomerOrder / 100;
            ToTalPayment = TotalDueBillOfCustomerOrder + CheckOutPercentVAT;
        }

        private void PrintBill()
        {
            FlowDocument fd = new FlowDocument();

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) 
                return;

            fd.PageHeight = pd.PrintableAreaWidth;
            fd.PageWidth = 325;
            fd.ColumnWidth = 325;
            fd.FontFamily = new FontFamily("Times New Roman");
            fd.LineHeight = 0.7;

            Paragraph para = new Paragraph(new Run("HIKI COFFEE SHOP"));
            para.FontSize = 17;
            para.TextAlignment = TextAlignment.Center;
            para.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(para);

            para = new Paragraph(new Run("Address: 330 West 42nd Street"));
            para.FontSize = 12;
            para.TextAlignment = TextAlignment.Center;
            para.Foreground = Brushes.Green;
            fd.Blocks.Add(para);

            para = new Paragraph(new Run("Phone: 0708046010"));
            para.LineHeight = 1;
            para.FontSize = 12;
            para.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(para);

            Paragraph paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Paragraph paraEnter = new Paragraph(new Run(""));
            paraEnter.FontSize = 6;
            paraEnter.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraEnter);

            Paragraph paraBill = new Paragraph(new Run("Coffee Bill"));
            paraBill.LineHeight = 1;
            paraBill.FontSize = 20;
            paraBill.TextAlignment = TextAlignment.Center;
            paraBill.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraBill);

            Paragraph paraIdBill = new Paragraph(new Run(BillCoffeeTable?.BillId.ToString()));
            paraIdBill.LineHeight = 1;
            paraIdBill.FontSize = 17;
            paraIdBill.FontWeight = FontWeights.Bold;
            paraIdBill.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraStaff = new Paragraph(new Run("       Staff: " + SystemConstants.UserIdInUse.ToString())); // + IdBillOfHuman.ToString()
            paraStaff.LineHeight = 1;
            paraStaff.FontSize = 13;
            paraStaff.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraStaff);

            paraIdBill = new Paragraph(new Run("       Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "             Time: " + DateTime.Now.ToString("HH:mm")));
            paraIdBill.LineHeight = 0.5;
            paraIdBill.FontSize = 13;
            paraIdBill.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraPartners = new Paragraph(new Run("       Customer: " + FullNameCustomerOrder)); //GetBillOfTableFurniture.Customer.DisplayName
            paraPartners.FontSize = 13;
            paraPartners.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraPartners);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            string[] arrColumnHeader = { "ITEM NAME", "QTY", "PRICE", "AMOUNT" };

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(26, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) });


            for (int i = 0; i < BillInfoCustomerOrders.Count + 2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            var t1 = new TextBlock() { Text = arrColumnHeader[0], Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1.SetValue(Grid.ColumnProperty, 0);
            t1.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t1);

            var t2 = new TextBlock() { Text = arrColumnHeader[1], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2.SetValue(Grid.ColumnProperty, 1);
            t2.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t2);

            var t3 = new TextBlock() { Text = arrColumnHeader[2], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3.SetValue(Grid.ColumnProperty, 2);
            t3.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t3);

            var t4 = new TextBlock() { Text = arrColumnHeader[3], Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4.SetValue(Grid.ColumnProperty, 3);
            t4.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t4);

            var t1Line = new TextBlock() { Text = "------------------------------------------------", Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1Line.SetValue(Grid.ColumnProperty, 0);
            t1Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t1Line);

            var t2Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2Line.SetValue(Grid.ColumnProperty, 1);
            t2Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t2Line);

            var t3Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3Line.SetValue(Grid.ColumnProperty, 2);
            t3Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t3Line);

            var t4Line = new TextBlock() { Text = "----------------------------------", Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4Line.SetValue(Grid.ColumnProperty, 3);
            t4Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t4Line);

            for (int i = 0; i < BillInfoCustomerOrders.Count; i++)
            {
                var item = new TextBlock() { Text = BillInfoCustomerOrders[i].NameProduct, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                item.SetValue(Grid.ColumnProperty, 0);
                item.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(item);

                var qty = new TextBlock() { Text = BillInfoCustomerOrders[i].Quantity.ToString("N0"), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                qty.SetValue(Grid.ColumnProperty, 1);
                qty.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(qty);

                var rate = new TextBlock() { Text = BillInfoCustomerOrders[i].Price.ToString("N0"), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                rate.SetValue(Grid.ColumnProperty, 2);
                rate.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(rate);


                var value = new TextBlock() { Text = BillInfoCustomerOrders[i].Amount.ToString("N0"), Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                value.SetValue(Grid.ColumnProperty, 3);
                value.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(value);
            }


            fd.Blocks.Add(new BlockUIContainer(grid));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Grid gridPayment = new Grid();

            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(220, GridUnitType.Star) });
            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90, GridUnitType.Star) });

            for (int i = 0; i < 5; i++)
            {
                gridPayment.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            ObservableCollection<PaymentBill> paymenBill = new ObservableCollection<PaymentBill>();
            paymenBill.Add(new PaymentBill() { DisplayName = "Sub Total:", Amount = TotalDueBillOfCustomerOrder });
            paymenBill.Add(new PaymentBill() { DisplayName = "VAT:", Amount = CheckOutPercentVAT });
            paymenBill.Add(new PaymentBill() { DisplayName = "Grand Total:", Amount = ToTalPayment });
            paymenBill.Add(new PaymentBill() { DisplayName = "Line", Amount = 0 });

            for (int i = 0; i < paymenBill.Count; i++)
            {
                if (i != 3)
                {
                    var displayName = new TextBlock() { Text = paymenBill[i].DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(15, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                    displayName.SetValue(Grid.ColumnProperty, 0);
                    displayName.FontSize = 14;
                    displayName.FontWeight = FontWeights.Bold;
                    displayName.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(displayName);

                    var amount = new TextBlock() { Text = paymenBill[i].Amount.ToString("N0"), Margin = new Thickness(2, 0, 25, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    amount.SetValue(Grid.ColumnProperty, 1);
                    amount.FontSize = 14;
                    amount.TextAlignment = TextAlignment.Right;
                    amount.FontWeight = FontWeights.Bold;
                    amount.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(amount);
                }
                else
                {
                    var lineHorizontal1 = new TextBlock() { Text = "-----------------------------------------------------------------", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12 };
                    lineHorizontal1.SetValue(Grid.ColumnProperty, 0);
                    lineHorizontal1.FontSize = 10;
                    lineHorizontal1.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal1);

                    var lineHorizontal2 = new TextBlock() { Text = "-----------------------------", Margin = new Thickness(-3, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
                    lineHorizontal2.SetValue(Grid.ColumnProperty, 1);
                    lineHorizontal2.FontSize = 10;
                    lineHorizontal2.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal2);
                }
            }
            fd.Blocks.Add(new BlockUIContainer(gridPayment));

            Paragraph paraContact = new Paragraph(new Run("In a city filled with so many choices, we thank for choosing us. Please feel free to contact us if " +
                "you encounter any problems related to our services."));
            paraContact.Margin = new Thickness(15, 0, 15, 0);
            paraContact.FontSize = 12;
            paraContact.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraContact);

            Paragraph paraHappy = new Paragraph(new Run("We are happy to serve you!"));
            paraHappy.FontSize = 12;
            paraHappy.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraHappy);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            paraLine.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraLine);


            IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }
    }

    public class PaymentBill : BindableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }
    }
}
