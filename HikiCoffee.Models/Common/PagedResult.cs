using System.Collections.ObjectModel;

namespace HikiCoffee.Models.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public ObservableCollection<T> Items { set; get; }
    }
}
