using System.Text.RegularExpressions;
using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email() { }  // EF Core için boş constructor

        public Email(string value)
        {
            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email format.");

            Value = value;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Statik fabrika metodu
        public static Email Create(string value)
        {
            return new Email(value);
        }
    }
}
