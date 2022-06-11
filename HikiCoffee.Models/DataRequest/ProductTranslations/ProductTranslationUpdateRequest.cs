namespace HikiCoffee.Models.DataRequest.ProductTranslations
{
    public class ProductTranslationUpdateRequest
    {
        public int ProductTranslationId { get; set; }

        public string NameProduct { get; set; }

        public string? Description { get; set; }

        public string? Details { get; set; }

        public string? SeoDescription { get; set; }

        public string? SeoTitle { get; set; }
    }
}
