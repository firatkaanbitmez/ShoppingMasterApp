namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Address
    {
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string City { get; }
        public string State { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string addressLine1, string addressLine2, string city, string state, string postalCode, string country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        // Equality override for ValueObject comparison
    }
}
