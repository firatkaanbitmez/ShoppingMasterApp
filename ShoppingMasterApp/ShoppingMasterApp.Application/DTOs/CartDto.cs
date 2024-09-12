using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
