using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class DeleteOrderCommand
    {
        public int Id { get; set; }
    }

}
