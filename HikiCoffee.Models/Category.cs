namespace HikiCoffee.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int SortOrder { get; set; }

        public bool? IsShowOnHome { get; set; }

        public int? ParentId { get; set; }

        public bool IsActive { get; set; }
    }
}
