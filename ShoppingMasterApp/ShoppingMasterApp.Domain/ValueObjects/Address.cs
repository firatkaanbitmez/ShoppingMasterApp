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
            if (string.IsNullOrWhiteSpace(addressLine1) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Address Line 1, City, and Country are required.");
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public override bool Equals(object obj)
        {
            if (obj is Address other)
                return AddressLine1 == other.AddressLine1 && City == other.City && Country == other.Country;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AddressLine1, City, Country);
        }
    }

}
