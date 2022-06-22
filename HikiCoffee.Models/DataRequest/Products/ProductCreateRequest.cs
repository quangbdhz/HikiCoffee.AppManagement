namespace HikiCoffee.Models.DataRequest.Products
{
    public class ProductCreateRequest
    {
        public string UrlImageCoverProduct { get; set; }

        public decimal Price { get; set; }

        public bool? IsFeatured { get; set; }

        public int? UnitId { get; set; }
    }
}
