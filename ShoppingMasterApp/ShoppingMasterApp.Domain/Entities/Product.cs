﻿using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; } 
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductDetails ProductDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
