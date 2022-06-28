namespace HikiCoffee.Models.DataRequest.Bills
{
    public class BillCreateRequest
    {
        public int CoffeeTableId { get; set; }

        public Guid UserCustomerId { get; set; }

        public Guid UserPaymentId { get; set; }
    }
}
