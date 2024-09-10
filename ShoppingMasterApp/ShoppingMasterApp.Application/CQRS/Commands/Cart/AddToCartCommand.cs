using FluentValidation;

namespace ShoppingMasterApp.Application.CQRS.Commands.Cart
{
    public class AddToCartCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }


}
