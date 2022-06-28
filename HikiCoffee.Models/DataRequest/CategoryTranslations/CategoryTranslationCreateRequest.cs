namespace HikiCoffee.Models.DataRequest.CategoryTranslations
{
    public class CategoryTranslationCreateRequest
    {
        public int CategoryId { get; set; }

        public string NameCategory { get; set; }

        public string SeoDescription { get; set; }

        public string SeoTitle { get; set; }

        public int LanguageId { get; set; }
    }
}
