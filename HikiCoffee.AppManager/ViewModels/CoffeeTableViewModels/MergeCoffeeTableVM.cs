using HikiCoffee.ApiIntegration.CoffeeTableAPI;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;

namespace HikiCoffee.AppManager.ViewModels.CoffeeTableViewModels
{
    public class MergeCoffeeTableVM : PageCoffeeTableVM
    {
        private ObservableCollection<CoffeeTable> _coffeeTableNotFounds;
        public ObservableCollection<CoffeeTable> CoffeeTableNotFounds
        {
            get { return _coffeeTableNotFounds; }
            set { SetProperty(ref _coffeeTableNotFounds, value); }
        }

        public DelegateCommand<CoffeeTable> GetSelectedCoffeeTableNotFoundCommand { get; set; }

        public DelegateCommand<Window> CloseMergeCoffeeTableWindowCommand { get; set; }


        private readonly ICoffeeTableAPI _coffeeTableAPI;

        public MergeCoffeeTableVM()
        {
            _coffeeTableAPI = new CoffeeTableAPI();

            Loaded();

            GetSelectedCoffeeTableNotFoundCommand = new DelegateCommand<CoffeeTable>((p) =>
            {
                if (p != null)
                {
                    StaticGetCoffeeTableMergeChange = p;
                }
            });

            CloseMergeCoffeeTableWindowCommand = new DelegateCommand<Window>((p) => { p.Close(); });
        }

        private async void Loaded()
        {
            CoffeeTableNotFounds = new ObservableCollection<CoffeeTable>();
            var reponse = await _coffeeTableAPI.GetAllCoffeeTable(SystemConstants.TokenInUse);

            foreach (var item in reponse)
            {
                if(StaticGetCoffeeTableChange != null)
                {
                    if (item.StatusId == 4 && StaticGetCoffeeTableChange.Id != item.Id)
                    {
                        CoffeeTableNotFounds.Add(item);
                    }
                }
            }

        }
    }
}
