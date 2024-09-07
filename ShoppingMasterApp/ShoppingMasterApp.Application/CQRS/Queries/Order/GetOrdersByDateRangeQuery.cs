using MediatR;
using ShoppingMasterApp.Application.DTOs;
using System;

namespace ShoppingMasterApp.Application.CQRS.Queries.Order
{
    public class GetOrdersByDateRangeQuery : IRequest<IEnumerable<OrderDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
