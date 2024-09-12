using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string CountryCode { get; private set; }
        public string Number { get; private set; }

        public PhoneNumber(string countryCode, string number)
        {
            if (!IsValidPhoneNumber(countryCode, number))
            {
                throw new ArgumentException("Invalid phone number.");
            }
            CountryCode = countryCode;
            Number = number;
        }

        private bool IsValidPhoneNumber(string countryCode, string number)
        {
            // Telefon numarası validasyonu
            return Regex.IsMatch(number, @"^\d+$");
        }
    }

}
