using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }  // Rating between 1-5 stars
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
