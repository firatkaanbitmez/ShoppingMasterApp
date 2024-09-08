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
        public decimal Amount { get; set; }  // Add this line
        public DateTime ValidUntil { get; set; }
    }

}
