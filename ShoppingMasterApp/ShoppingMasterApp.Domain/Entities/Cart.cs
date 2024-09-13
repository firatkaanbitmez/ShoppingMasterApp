using ShoppingMasterApp.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(item => item.TotalPrice);
            }
        }
    }
}
