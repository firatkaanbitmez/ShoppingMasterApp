using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
