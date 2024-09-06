using ShoppingMasterApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Discount : BaseEntity, IAggregateRoot
    {
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsUsed { get; set; }
    }

}
