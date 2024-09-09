using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Category
{
    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        // Add ProductDetails properties
        public string Manufacturer { get; set; }
        public string Sku { get; set; }
    }

}
