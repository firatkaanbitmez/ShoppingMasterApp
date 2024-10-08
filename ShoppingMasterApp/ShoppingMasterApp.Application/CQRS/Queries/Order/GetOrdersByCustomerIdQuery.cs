﻿using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Order
{
    public class GetOrdersByCustomerIdQuery : IRequest<List<OrderDto>>
    {
        public int CustomerId { get; set; }

        public class Handler : IRequestHandler<GetOrdersByCustomerIdQuery, List<OrderDto>>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<List<OrderDto>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
            {
                var orders = await _orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId);

                return orders.Select(order => new OrderDto
                {
                    Id = order.Id,
                    ProductId = order.OrderItems.FirstOrDefault()?.ProductId ?? 0,
                    ProductName = order.OrderItems.FirstOrDefault()?.Product?.Name,
                    Quantity = order.OrderItems.FirstOrDefault()?.Quantity ?? 0,
                    TotalPrice = order.TotalAmount.Amount
                }).ToList();
            }
        }
    }
}
