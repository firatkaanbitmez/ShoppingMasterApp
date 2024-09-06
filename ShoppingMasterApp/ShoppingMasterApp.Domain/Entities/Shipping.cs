using ShoppingMasterApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Shipping : BaseEntity, IAggregateRoot
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }  // Hazırlanıyor, Kargoya verildi, Teslim edildi
        public DateTime ShippedDate { get; set; }
    }

}
