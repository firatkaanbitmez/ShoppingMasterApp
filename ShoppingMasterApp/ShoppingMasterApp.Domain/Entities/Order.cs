using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);  // Dynamic total amount
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
}
