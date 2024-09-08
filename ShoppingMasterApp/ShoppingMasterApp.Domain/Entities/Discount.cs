using ShoppingMasterApp.Domain.Common;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }  // Add this line
        public string Code { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
