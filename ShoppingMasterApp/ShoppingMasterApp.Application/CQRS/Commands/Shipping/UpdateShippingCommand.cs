using System;

namespace ShoppingMasterApp.Application.CQRS.Commands.Shipping
{
    public class UpdateShippingCommand
    {
        public int ShippingId { get; set; }
        public string Status { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public UpdateShippingCommand(int shippingId, string status, string addressLine1, string addressLine2, string city, string state, string postalCode, string country)
        {
            ShippingId = shippingId;
            Status = status;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
