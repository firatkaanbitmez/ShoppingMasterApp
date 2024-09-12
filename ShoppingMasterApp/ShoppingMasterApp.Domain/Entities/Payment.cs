using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;
using ShoppingMasterApp.Domain.Enums;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Money Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
