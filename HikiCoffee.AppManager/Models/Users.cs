using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace HikiCoffee.AppManager.Models
{
    public class Users : BindableBase
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public IList<string> Roles { get; set; }
    }
}
