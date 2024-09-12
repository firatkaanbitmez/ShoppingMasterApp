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
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Money TotalAmount { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
