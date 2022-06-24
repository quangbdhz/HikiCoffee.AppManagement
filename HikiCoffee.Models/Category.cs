using Prism.Mvvm;

namespace HikiCoffee.Models
{
    public class Category : BindableBase
    {
        public int Id { get; set; }

        public int SortOrder { get; set; }

        private bool? _isShowOnHome;
        public bool? IsShowOnHome
        {
            get { return _isShowOnHome; }
            set { SetProperty(ref _isShowOnHome, value); }
        }

        public int? ParentId { get; set; }

        public bool IsActive { get; set; }

        public string? UrlImageCoverCategory { get; set; }
    }
}
