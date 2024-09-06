﻿using ShoppingMasterApp.Domain.Common;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
