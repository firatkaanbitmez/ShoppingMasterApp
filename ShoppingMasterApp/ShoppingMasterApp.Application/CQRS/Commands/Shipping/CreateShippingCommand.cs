using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Shipping
{
    public class CreateShippingCommand
    {
        public int OrderId { get; internal set; }
        public string Status { get; internal set; }
        public string AddressLine1 { get; internal set; }
        public string AddressLine2 { get; internal set; }
        public string City { get; internal set; }
        public string State { get; internal set; }
        public string PostalCode { get; internal set; }
        public string Country { get; internal set; }
    }
}
