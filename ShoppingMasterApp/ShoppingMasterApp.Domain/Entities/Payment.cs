using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Money Amount { get; set; }  // Using Money as a ValueObject
        public DateTime PaymentDate { get; set; }
        public PaymentDetails PaymentDetails { get; set; }  // Payment method details
        public bool IsSuccessful { get; set; }
    }
}
