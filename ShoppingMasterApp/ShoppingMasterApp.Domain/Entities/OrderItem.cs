using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderItem(int productId, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }

}
