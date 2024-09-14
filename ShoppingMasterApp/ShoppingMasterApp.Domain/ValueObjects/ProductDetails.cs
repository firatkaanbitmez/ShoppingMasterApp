using System;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class ProductDetails
    {
        public string Manufacturer { get; private set; }
        public string Sku { get; private set; }  

        public ProductDetails(string manufacturer, string sku)
        {
            if (string.IsNullOrEmpty(manufacturer)) throw new ArgumentException("Manufacturer cannot be empty");
            if (string.IsNullOrEmpty(sku)) throw new ArgumentException("Sku cannot be empty");

            Manufacturer = manufacturer;
            Sku = sku;
        }

    }
}