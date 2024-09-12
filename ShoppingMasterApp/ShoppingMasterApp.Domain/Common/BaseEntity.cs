
using System;

namespace ShoppingMasterApp.Domain.Common
{
    public abstract class BaseEntity
    {
        public int DisplayId { get; set; }
        public Guid GuidID { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
    }
}
