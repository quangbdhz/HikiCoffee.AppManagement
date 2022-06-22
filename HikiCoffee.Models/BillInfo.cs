using Prism.Mvvm;

namespace HikiCoffee.Models
{
    public class BillInfo : BindableBase
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public int ProductId { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        public decimal Price { get; set; }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        public string NameProduct { get; set; }
    }
}
