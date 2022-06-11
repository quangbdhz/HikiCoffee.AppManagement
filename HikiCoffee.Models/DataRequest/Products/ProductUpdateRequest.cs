namespace HikiCoffee.Models.DataRequest.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }

        public string UrlImageCoverProduct { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }

        public bool? IsFeatured { get; set; }
    }
}
