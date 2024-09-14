using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money() { }  // EF Core için boş constructor

        public Money(decimal amount, string currency)
        {
            ValidateMoney(amount, currency);

            Amount = amount;
            Currency = currency;
        }

        private void ValidateMoney(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");
        }

        // Statik fabrika metodu
        public static Money Create(decimal amount, string currency)
        {
            return new Money(amount, currency);
        }
    }
}
