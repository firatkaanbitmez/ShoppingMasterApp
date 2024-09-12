using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Code { get; set; }
        public DiscountType Type { get; set; }
        public DateTime ValidUntil { get; set; }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return Type switch
            {
                DiscountType.Percentage => originalPrice - (originalPrice * Amount / 100),
                DiscountType.FixedAmount => originalPrice - Amount,
                _ => originalPrice
            };
        }
    }
}
