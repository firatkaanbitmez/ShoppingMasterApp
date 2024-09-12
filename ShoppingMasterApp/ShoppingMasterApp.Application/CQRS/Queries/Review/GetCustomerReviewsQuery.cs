using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Review
{
    public class GetCustomerReviewsQuery
    {
        public int CustomerId { get; set; }
    }
}
