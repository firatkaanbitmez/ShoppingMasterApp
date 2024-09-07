using MediatR;
using ShoppingMasterApp.Application.DTOs;

namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class AddOrderItemCommand : IRequest<OrderDto>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
