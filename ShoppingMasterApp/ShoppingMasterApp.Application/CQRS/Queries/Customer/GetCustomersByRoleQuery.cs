using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetCustomersByRoleQuery
    {
        public Roles Role { get; set; }
    }

    public class GetUsersByRoleQueryResponse
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}
