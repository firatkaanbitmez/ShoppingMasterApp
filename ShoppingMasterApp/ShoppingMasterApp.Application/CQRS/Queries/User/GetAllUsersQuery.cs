using ShoppingMasterApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetAllUsersQuery
    {
    }
    public class GetAllUsersQueryResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
