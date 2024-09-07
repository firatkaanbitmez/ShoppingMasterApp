using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
                throw new ArgumentException("Invalid phone number");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
