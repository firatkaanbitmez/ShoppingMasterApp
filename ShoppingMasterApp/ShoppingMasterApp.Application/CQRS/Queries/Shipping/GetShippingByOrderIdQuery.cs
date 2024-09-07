using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Shipping
{
    public class GetShippingByOrderIdQuery
    {
        public int OrderId { get; set; }
    }
}
