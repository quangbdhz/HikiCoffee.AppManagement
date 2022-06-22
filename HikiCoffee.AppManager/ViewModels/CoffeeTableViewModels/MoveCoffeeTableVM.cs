using HikiCoffee.ApiIntegration.CoffeeTableAPI;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HikiCoffee.AppManager.ViewModels.CoffeeTableViewModels
{
    public class MoveCoffeeTableVM : PageCoffeeTableVM
    {
        private ObservableCollection<CoffeeTable> _coffeeTableFounds;
        public ObservableCollection<CoffeeTable> CoffeeTableFounds
        {
            get { return _coffeeTableFounds; }
            set { SetProperty(ref _coffeeTableFounds, value); }
        }

        public DelegateCommand<CoffeeTable> GetSelectedCoffeeTableFoundCommand { get; set; }

        public DelegateCommand<Window> CloseMoveCoffeeTableWindowCommand { get; set; }

        private readonly ICoffeeTableAPI _coffeeTableAPI;

        public MoveCoffeeTableVM()
        {
            _coffeeTableAPI = new CoffeeTableAPI();

            Loaded();

            GetSelectedCoffeeTableFoundCommand = new DelegateCommand<CoffeeTable>((p) =>
            {
                if(p != null)
                {
                    StaticGetCoffeeTableMoveChange = p;
                }
            });

            CloseMoveCoffeeTableWindowCommand = new DelegateCommand<Window>((p) => { p.Close(); });
        }

        private async void Loaded()
        {
            CoffeeTableFounds = new ObservableCollection<CoffeeTable>();
            var reponse = await _coffeeTableAPI.GetAllCoffeeTable(SystemConstants.TokenInUse);

            foreach(var item in reponse)
            {
                if(item.StatusId == 3)
                {
                    CoffeeTableFounds.Add(item);
                }
            }

        }
    }
}
