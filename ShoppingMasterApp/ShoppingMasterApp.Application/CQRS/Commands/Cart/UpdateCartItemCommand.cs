using MediatR;
using ShoppingMasterApp.Application.DTOs;

namespace ShoppingMasterApp.Application.CQRS.Commands.Cart
{
    public class UpdateCartItemCommand : IRequest<CartDto>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
