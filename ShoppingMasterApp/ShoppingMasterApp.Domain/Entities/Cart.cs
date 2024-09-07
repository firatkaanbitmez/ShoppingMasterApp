using ShoppingMasterApp.Domain.Common;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        // Sum up decimal amounts for TotalPrice
        public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
    }
}
