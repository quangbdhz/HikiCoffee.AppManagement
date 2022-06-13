namespace HikiCoffee.Models
{
    public class Suplier
    {
        public int Id { get; set; }

        public string NameSuplier { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string MoreInfo { get; set; }

        public DateTime ContractDate { get; set; }

        public bool IsActive { get; set; }
    }
}
