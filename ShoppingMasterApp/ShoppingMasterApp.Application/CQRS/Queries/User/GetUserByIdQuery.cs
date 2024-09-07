using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetUserByIdQuery
    {
        public int Id { get; set; }
    }
}
