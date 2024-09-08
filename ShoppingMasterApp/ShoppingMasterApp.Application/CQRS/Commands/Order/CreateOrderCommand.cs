using ShoppingMasterApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Order
{
    public class CreateOrderCommand
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }


}
