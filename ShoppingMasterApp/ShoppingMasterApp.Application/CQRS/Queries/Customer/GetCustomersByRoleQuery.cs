using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.Customer
{
    public class GetCustomersByRoleQuery
    {
        public Roles Role { get; set; }
    }

    public class GetCustomersByRoleQueryResponse
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}
