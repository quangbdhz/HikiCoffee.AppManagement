namespace HikiCoffee.Models.DataRequest.CategoryTranslations
{
    public class CategoryTranslationUpdateRequest
    {
        public int Id { get; set; }

        public string NameCategory { get; set; }

        public string SeoDescription { get; set; }

        public string SeoTitle { get; set; }
    }
}
