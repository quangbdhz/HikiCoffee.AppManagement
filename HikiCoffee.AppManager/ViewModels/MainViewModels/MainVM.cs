using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Controls;
using System;

namespace HikiCoffee.AppManager.ViewModels.MainViewModels
{

    public class MainVM : BindableBase
    {
        private string? _sourcePage;
        public string? SourcePage
        {
            get { return _sourcePage; }
            set { SetProperty(ref _sourcePage, value); }
        }

        private int _optionMenu;
        public int OptionMenu
        {
            get { return _optionMenu; }
            set { SetProperty(ref _optionMenu, value); }
        }

        #region color_icon_listview
        private string? _colorIconHome;
        public string? ColorIconHome
        {
            get { return _colorIconHome; }
            set { SetProperty(ref _colorIconHome, value); }
        }

        private string? _colorIconTableFurniture;
        public string? ColorIconTableFurniture
        {
            get { return _colorIconTableFurniture; }
            set { SetProperty(ref _colorIconTableFurniture, value); }
        }

        private string? _colorIconDrink;
        public string? ColorIconDrink
        {
            get { return _colorIconDrink; }
            set { SetProperty(ref _colorIconDrink, value); }
        }

        private string? _colorIconFood;
        public string? ColorIconFood
        {
            get { return _colorIconFood; }
            set { SetProperty(ref _colorIconFood, value); }
        }

        private string? _colorIconCustomer;
        public string? ColorIconCustomer
        {
            get { return _colorIconCustomer; }
            set { SetProperty(ref _colorIconCustomer, value); }
        }

        private string? _colorIconStatistical;
        public string? ColorIconStatistical
        {
            get { return _colorIconStatistical; }
            set { SetProperty(ref _colorIconStatistical, value); }
        }

        private string? _colorIconBill;
        public string? ColorIconBill
        {
            get { return _colorIconBill; }
            set { SetProperty(ref _colorIconBill, value); }
        }

        private string? _colorIconDelete;
        public string? ColorIconDelete
        {
            get { return _colorIconDelete; }
            set { SetProperty(ref _colorIconDelete, value); }
        }

        private string? _urlAvatarStaffLogin;
        public string? UrlAvatarStaffLogin
        {
            get { return _urlAvatarStaffLogin; }
            set { SetProperty(ref _urlAvatarStaffLogin, value); }
        }
        #endregion


        public DelegateCommand<ListView> ChoosePageCommand { get; set; }

        public MainVM()
        {
            SourcePage = @"/Views/MainViews/Pages/MainPage.xaml";

            ColorIconHome = "Black";
            ColorIconTableFurniture = "White";
            ColorIconFood = "White";
            ColorIconDrink = "White";
            ColorIconCustomer = "White";
            ColorIconStatistical = "White";
            ColorIconBill = "White";
            ColorIconDelete = "White";

            ChoosePageCommand = new DelegateCommand<ListView>(ExcuteChoosePage);
        }

        private void ExcuteChoosePage(ListView obj)
        {
            OptionMenu = obj.SelectedIndex;

            if (OptionMenu == 0)
            {
                SourcePage = @"/Views/MainViews/Pages/MainPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 1)
            {
                SourcePage = @"/Views/MainViews/Pages/TableFurniturePage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 2)
            {
                SourcePage = @"/Views/MainViews/Pages/ProductPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 3)
            {
                SourcePage = @"/Views/MainViews/Pages/ImportProductPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 4)
            {
                SourcePage = @"/Views/MainViews/Pages/CustomerPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 5)
            {
                SourcePage = @"/Views/MainViews/Pages/CustomerPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 6)
            {
                SourcePage = @"/Views/MainViews/Pages/CustomerPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else if (OptionMenu == 7)
            {
                SourcePage = @"/Views/MainViews/Pages/CustomerPage.xaml";
                ClickChangeColorIcon(OptionMenu);
            }
            else
            {

            }
        }

        public void ClickChangeColorIcon(int index)
        {
            string[] arrColor = { "White", "White", "White", "White", "White", "White", "White", "White" };

            arrColor[index] = "Black";

            ColorIconHome = arrColor[0];
            ColorIconTableFurniture = arrColor[1];
            ColorIconDrink = arrColor[2];
            ColorIconFood = arrColor[3];
            ColorIconCustomer = arrColor[4];
            ColorIconStatistical = arrColor[5];
            ColorIconBill = arrColor[6];
            ColorIconDelete = arrColor[7];
        }
    }
}
