using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class ChangeProductStockCommand
    {
        public int ProductId { get; set; }
    }
}
