using MediatR;
using ShoppingMasterApp.Application.DTOs;

namespace ShoppingMasterApp.Application.CQRS.Queries.Cart
{
    public class GetCartItemsQuery : IRequest<CartDto>
    {
        public int UserId { get; set; }
    }
}
