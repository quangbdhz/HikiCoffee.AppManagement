namespace HikiCoffee.Models.DataRequest.Bills
{
    public class BillCheckOutRequest
    {
        public int BillId { get; set; }

        public decimal TotalPayPrice { get; set; }
    }
}
