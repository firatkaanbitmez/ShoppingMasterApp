using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class ProductDetails
    {
        public string Manufacturer { get; private set; }
        public string Sku { get; private set; }

        public ProductDetails(string manufacturer, string sku)
        {
            Manufacturer = manufacturer;
            Sku = sku;
        }
    }
}
