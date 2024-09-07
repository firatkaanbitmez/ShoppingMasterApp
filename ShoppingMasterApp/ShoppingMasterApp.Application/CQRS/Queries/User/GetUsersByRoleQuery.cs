using ShoppingMasterApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetUsersByRoleQuery
    {
        public Roles Roles { get; set; }
    }
}
