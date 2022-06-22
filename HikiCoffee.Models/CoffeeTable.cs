using Prism.Mvvm;

namespace HikiCoffee.Models
{
    public class CoffeeTable : BindableBase
    {
        public int Id { get; set; }

        public string NameCoffeeTable { get; set; }

        public DateTime? AppointmentTime { get; set; }

        public DateTime? ExpirationTime { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private int _statusId;
        public int StatusId
        {
            get { return _statusId; }
            set { SetProperty(ref _statusId, value); }
        }

        public string NameStatus { get; set; }
    }
}
