using System.Text.RegularExpressions;
using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email() { }

        public Email(string value)
        {
            // Trim the input to remove leading/trailing whitespaces
            value = value.Trim();

            // Validate email format
            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email format.");

            Value = value;
        }

        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate the email format
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
