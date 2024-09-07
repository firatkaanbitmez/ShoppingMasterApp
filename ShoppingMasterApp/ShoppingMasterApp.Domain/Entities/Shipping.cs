using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Shipping : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Address ShippingAddress { get; set; }  // ValueObject for address
        public string Status { get; set; }  // Status like 'Shipped', 'In Process', 'Delivered'
        public DateTime ShippedDate { get; set; }
    }
}
