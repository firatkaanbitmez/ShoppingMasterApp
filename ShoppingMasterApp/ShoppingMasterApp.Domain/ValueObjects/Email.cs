﻿namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new ArgumentException("Invalid email address");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
