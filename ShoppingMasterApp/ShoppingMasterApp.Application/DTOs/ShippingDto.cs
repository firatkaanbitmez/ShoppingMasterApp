using System;

namespace ShoppingMasterApp.Application.DTOs
{
    public class ShippingDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime ShippedDate { get; set; }
        public string ShippingAddress { get; set; }
    }
}
