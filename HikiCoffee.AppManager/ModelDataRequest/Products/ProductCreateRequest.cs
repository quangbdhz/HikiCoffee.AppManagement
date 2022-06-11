namespace HikiCoffee.AppManager.ModelDataRequest.Products
{
    public class ProductCreateRequest
    {
        public string UrlImageCoverProduct { get; set; }

        public decimal Price { get; set; }

        public bool? IsFeatured { get; set; }
    }
}
