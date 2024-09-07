using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Discount
{
    public class CreateDiscountCommand 
    {
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
