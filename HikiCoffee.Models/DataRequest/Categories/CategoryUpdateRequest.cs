namespace HikiCoffee.Models.DataRequest.Categories
{
    public class CategoryUpdateRequest
    {
        public int Id { get; set; }

        public bool? IsShowOnHome { get; set; }

        public int? ParentId { get; set; }
    }
}
