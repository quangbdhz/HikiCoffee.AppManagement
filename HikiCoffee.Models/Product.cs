namespace HikiCoffee.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string UrlImageCoverProduct { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }

        public int Stock { get; set; }

        public int ViewCount { get; set; }

        public DateTime DateCreated { get; set; }

        public bool? IsFeatured { get; set; }

        public bool IsActive { get; set; }
    }
}
