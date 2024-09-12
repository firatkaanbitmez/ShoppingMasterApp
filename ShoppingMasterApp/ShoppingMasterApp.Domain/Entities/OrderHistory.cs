using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class OrderHistory : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderStatus PreviousStatus { get; set; }
        public OrderStatus NewStatus { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string ChangedBy { get; set; }
        public string Reason { get; set; } // Durum değişikliği sebebi
    }
}
