namespace ShoppingMasterApp.Domain.Models
{
    public class PagedQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedQuery()
        {
            PageNumber = 1; // Default page number
            PageSize = 10; // Default page size
        }
    }
}
