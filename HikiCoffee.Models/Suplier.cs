using Prism.Mvvm;

namespace HikiCoffee.Models
{
    public class Suplier : BindableBase
    {
        public int Id { get; set; }

        public string NameSuplier { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string MoreInfo { get; set; }

        public DateTime ContractDate { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }
    }
}
