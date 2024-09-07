using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Payment
{
    public class GetPaymentStatusQuery
    {
        public int OrderId { get; set; }
    }
}
