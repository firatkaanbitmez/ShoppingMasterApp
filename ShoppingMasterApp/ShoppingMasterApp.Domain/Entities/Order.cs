using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; }
        public Money TotalAmount { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public void CompleteOrder()
        {
            if (Payment == null || !Payment.IsSuccessful)
                throw new InvalidOperationException("Payment must be successful to complete the order.");

            Status = OrderStatus.Completed;
            // Other business logic for completing the order
        }

        public void CancelOrder()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Cannot cancel an order that has already been shipped or delivered.");

            Status = OrderStatus.Cancelled;
        }
    }
}
