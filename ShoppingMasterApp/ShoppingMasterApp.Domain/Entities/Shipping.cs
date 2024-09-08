using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Shipping : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Address ShippingAddress { get; set; }  
        public string Status { get; set; }  
        public DateTime ShippedDate { get; set; }
    }
}
