namespace HikiCoffee.Models.DataRequest.BillInfos
{
    public class BillInfoCreateRequest
    {
        public int BillId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }
    }
}
