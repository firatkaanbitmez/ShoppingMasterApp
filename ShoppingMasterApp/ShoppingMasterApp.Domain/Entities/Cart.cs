using ShoppingMasterApp.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice
        {
            get { return CartItems.Sum(item => item.TotalPrice); }
        }

        public void AddItem(CartItem item)
        {
            if (item.Quantity > item.Product.Stock)
                throw new InvalidOperationException("Not enough stock for product.");

            CartItems.Add(item);
        }

        public void RemoveItem(CartItem item)
        {
            CartItems.Remove(item);
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }
}
