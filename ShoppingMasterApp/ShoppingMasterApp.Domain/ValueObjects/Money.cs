﻿using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required");
            Amount = amount;
            Currency = currency;
        }
    }
}
