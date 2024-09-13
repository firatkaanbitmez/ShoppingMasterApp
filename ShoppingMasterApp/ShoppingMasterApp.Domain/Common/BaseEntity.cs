using System;

namespace ShoppingMasterApp.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }= Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } =true;
        
    }
}
