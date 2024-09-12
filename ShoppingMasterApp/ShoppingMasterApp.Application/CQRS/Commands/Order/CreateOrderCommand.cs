using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto ShippingAddress { get; set; }

        public class Handler : IRequestHandler<CreateOrderCommand, int>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
            {
                _orderRepository = orderRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = new ShoppingMasterApp.Domain.Entities.Order
                {
                    UserId = request.UserId,
                    OrderDate = DateTime.UtcNow,
                    Shipping = new Shipping
                    {
                        ShippingAddress = new Address(
                            request.ShippingAddress.AddressLine1,
                            request.ShippingAddress.AddressLine2,
                            request.ShippingAddress.City,
                            request.ShippingAddress.State,
                            request.ShippingAddress.PostalCode,
                            request.ShippingAddress.Country
                        ),
                        Status = ShippingStatus.Preparing,
                        ShippingCost = CalculateShippingCost(request.ShippingAddress)
                    },
                    OrderItems = request.OrderItems.Select(item => new OrderItem(item.ProductId, item.UnitPrice, item.Quantity)).ToList(),
                    TotalAmount = new Money(request.OrderItems.Sum(i => i.TotalPrice), "USD")
                };

                await _orderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return order.Id;  // Returning the newly created order ID
            }

            private decimal CalculateShippingCost(AddressDto address)
            {
                // Implement shipping cost calculation logic
                return 10.0m; // Placeholder shipping cost
            }
        }
    }
}
