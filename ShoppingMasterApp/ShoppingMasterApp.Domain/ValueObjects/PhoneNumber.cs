using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string CountryCode { get; private set; }
        public string Number { get; private set; }

        public PhoneNumber(string countryCode, string number)
        {
            if (string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(number))
            {
                throw new ArgumentException("Invalid phone number.");
            }
            CountryCode = countryCode;
            Number = number;
        }
    }
}
