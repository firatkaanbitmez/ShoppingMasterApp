using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Money TotalAmount { get; set; } 
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
}
