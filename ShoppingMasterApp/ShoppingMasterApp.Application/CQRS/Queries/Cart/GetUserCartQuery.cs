using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Cart
{
    public class GetUserCartQuery
    {
        public int UserId { get; set; }
    }
}
