using Prism.Mvvm;

namespace HikiCoffee.Models
{
    public class User : BindableBase
    {
        public Guid Id { get; set; }

        public string? UrlImageUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        private bool _isEmailConfirmed;
        public bool IsEmailConfirmed
        {
            get { return _isEmailConfirmed; }
            set { SetProperty(ref _isEmailConfirmed, value); }
        }

        public DateTime Dob { get; set; }

        public string NameGender { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        //public string NameRole { get; set; }
    }
}
