using ShoppingMasterApp.Domain.Common;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }  
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
