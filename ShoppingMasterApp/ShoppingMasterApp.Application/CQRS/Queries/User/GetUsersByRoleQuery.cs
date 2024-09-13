using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetUsersByRoleQuery
    {
        public Roles Role { get; set; }
    }

    public class GetUsersByRoleQueryResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
