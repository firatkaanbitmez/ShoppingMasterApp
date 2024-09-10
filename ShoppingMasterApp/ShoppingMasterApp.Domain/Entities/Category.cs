﻿using ShoppingMasterApp.Domain.Common;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
