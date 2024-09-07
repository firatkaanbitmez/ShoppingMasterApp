using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Money TotalAmount { get; set; }  // Money Value Object
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
}
