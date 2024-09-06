using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
