using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Review
{
    public class DeleteReviewCommand
    {
        public int Id { get; set; }
    }
}
