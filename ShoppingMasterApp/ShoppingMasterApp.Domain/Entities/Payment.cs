using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Money Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
