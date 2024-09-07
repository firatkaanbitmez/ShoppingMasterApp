using ShoppingMasterApp.Application.DTOs;
using System;

namespace ShoppingMasterApp.Application.CQRS.Queries.Order
{
    public class GetOrdersByDateRangeQuery 
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
