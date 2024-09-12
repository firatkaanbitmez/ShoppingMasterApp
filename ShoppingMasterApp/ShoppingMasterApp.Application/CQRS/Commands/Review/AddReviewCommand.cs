using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Review
{
    public class AddReviewCommand
    {
        public int Rating { get; internal set; }
        public string Comment { get; internal set; }
        public int ProductId { get; internal set; }
        public int CustomerId { get; internal set; }
    }
}
