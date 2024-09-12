using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects; // Assuming Address is defined in this namespace
namespace ShoppingMasterApp.Domain.Entities
{
    public class Shipping : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ShippingStatus Status { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal ShippingCost { get; set; }
        public string ShippingCompany { get; set; }
        public Address ShippingAddress { get; set; } // Use the Address value object

        public void UpdateStatus(ShippingStatus newStatus)
        {
            Status = newStatus;
            if (newStatus == ShippingStatus.Shipped)
            {
                ShippedDate = DateTime.UtcNow;
            }
        }
    }

}
