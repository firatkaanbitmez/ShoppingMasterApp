using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Review
{
    public class GetReviewByProductIdQuery
    {
        public int ProductId { get; set; }
    }
}
