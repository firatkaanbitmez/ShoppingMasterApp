using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Order
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }

        public class Handler : IRequestHandler<GetOrderByIdQuery, OrderDto>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");
                }

                return new OrderDto
                {
                    Id = order.Id,
                    ProductId = order.OrderItems.FirstOrDefault()?.ProductId ?? 0,
                    ProductName = order.OrderItems.FirstOrDefault()?.Product?.Name,
                    Quantity = order.OrderItems.FirstOrDefault()?.Quantity ?? 0,
                    TotalPrice = order.TotalAmount.Amount
                };
            }
        }
    }
}
