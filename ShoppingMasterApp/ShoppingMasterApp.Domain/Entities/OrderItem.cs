using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // Toplam fiyat dinamik olarak hesaplanıyor.
        public decimal TotalPrice => UnitPrice * Quantity;

        public int OrderId { get; set; }

        // Constructor eklendi
        public OrderItem(int productId, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        // Ürün ve miktar güncellemesi için gerekli metod eklendi
        public void UpdateOrderItem(int quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
