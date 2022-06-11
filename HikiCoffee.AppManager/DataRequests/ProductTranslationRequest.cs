namespace HikiCoffee.AppManager.DataRequests
{
    public class ProductTranslationRequest
    {
        public int ProductId { get; set; }

        public string NameProduct { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public string SeoDescription { get; set; }

        public string SeoTitle { get; set; }

        public int LanguageId { get; set; }
    }
}
