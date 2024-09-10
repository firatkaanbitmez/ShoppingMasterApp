using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Cart
{
    public class RemoveFromCartCommand
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }

}
