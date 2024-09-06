using ShoppingMasterApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Payment : BaseEntity, IAggregateRoot
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }  // Kredi kartı, PayPal, vb.
        public bool IsSuccessful { get; set; }
    }

}
