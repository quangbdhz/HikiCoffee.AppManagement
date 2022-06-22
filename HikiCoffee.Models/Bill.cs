namespace HikiCoffee.Models
{
    public class Bill
    {
        public int BillId { get; set; }

        public int CoffeeTableId { get; set; }

        public Guid UserCustomerId { get; set; }

        public string NameCustomer { get; set; }

        public DateTime DateCheckIn { get; set; }

        public DateTime? DateCheckOut { get; set; }

        public Guid UserPaymentId { get; set; }

        public decimal TotalPayPrice { get; set; }

        public int StatusId { get; set; }
    }
}
