using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class RemoveOrderItemCommand
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
