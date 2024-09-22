using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string CountryCode { get; private set; }
        public string Number { get; private set; }

        private PhoneNumber() { }  // EF Core için boş constructor

        public PhoneNumber(string countryCode, string number)
        {
            ValidatePhoneNumber(countryCode, number);

            CountryCode = countryCode;
            Number = number;
        }

        private void ValidatePhoneNumber(string countryCode, string number)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country Code is required.");
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone Number is required.");
        }

        // Statik fabrika metodu
        public static PhoneNumber Create(string countryCode, string number)
        {
            return new PhoneNumber
            {
                CountryCode = countryCode,
                Number = number
            };
        }
    }
}
