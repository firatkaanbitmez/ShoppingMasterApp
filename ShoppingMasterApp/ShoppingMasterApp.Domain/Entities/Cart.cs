using ShoppingMasterApp.Domain.Common;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; private set; }  // TotalPrice dışarıdan set edilemez, yalnızca hesaplama ile güncellenir.

        public void CalculateTotalPrice()
        {
            TotalPrice = CartItems.Sum(item => item.UnitPrice * item.Quantity);
        }

        // Ürün ekleme veya güncelleme işlemi
        public void AddOrUpdateItem(Product product, int quantity)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

            if (existingItem != null) // Eğer ürün zaten varsa güncelle
            {
                existingItem.Quantity += quantity;
            }
            else // Eğer ürün yoksa ekle
            {
                var newItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.Price.Amount
                };
                CartItems.Add(newItem);
            }

            CalculateTotalPrice(); // Toplam fiyatı güncelle
        }

        // Ürün silme işlemi
        public void RemoveItem(int productId)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                CartItems.Remove(existingItem);
                CalculateTotalPrice(); // Toplam fiyatı güncelle
            }
        }

        // Sepeti tamamen temizleme işlemi
        public void Clear()
        {
            CartItems.Clear();
            CalculateTotalPrice();  // TotalPrice sıfırlanır
        }
    }




}
