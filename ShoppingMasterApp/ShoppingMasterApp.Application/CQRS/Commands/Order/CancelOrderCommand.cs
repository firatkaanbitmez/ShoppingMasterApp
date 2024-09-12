using MediatR;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public class Handler : IRequestHandler<CancelOrderCommand, bool>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
            {
                _orderRepository = orderRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                if (order == null || order.Shipping.Status != ShippingStatus.Preparing)
                {
                    throw new InvalidOperationException("Order cannot be cancelled");
                }

                order.Payment.IsSuccessful = false;
                order.Shipping.UpdateStatus(ShippingStatus.Preparing); // Revert shipping status
                order.MarkAsDeleted();

                _orderRepository.Update(order);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
        }
    }

}
