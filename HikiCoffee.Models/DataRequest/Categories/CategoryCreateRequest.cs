namespace HikiCoffee.Models.DataRequest.Categories
{
    public class CategoryCreateRequest
    {
        public bool? IsShowOnHome { get; set; }

        public int? ParentId { get; set; }

        public string? UrlImageCoverCategory { get; set; }
    }
}
