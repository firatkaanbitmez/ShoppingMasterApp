using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Review
{
    public class UpdateReviewCommand
    {
        public int Id { get; set; }
        public string Comment { get; internal set; }
        public int Rating { get; internal set; }
    }
}
