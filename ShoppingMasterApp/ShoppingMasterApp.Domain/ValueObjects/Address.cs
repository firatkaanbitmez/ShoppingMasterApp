using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Address
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        // Parametresiz constructor - EF Core'un ihtiyaç duyduğu
        private Address() { }

        // Parametreli constructor
        public Address(string addressLine1, string addressLine2, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(addressLine1)) throw new ArgumentException("Address Line 1 is required");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal Code is required");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");

            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
