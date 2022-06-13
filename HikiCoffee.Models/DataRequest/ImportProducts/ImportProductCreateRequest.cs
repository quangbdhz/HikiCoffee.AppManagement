namespace HikiCoffee.Models.DataRequest.ImportProducts
{
    public class ImportProductCreateRequest
    {
        public Guid UserIdImportProduct { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int PriceImportProduct { get; set; }

        public int SuplierId { get; set; }
    }
}
