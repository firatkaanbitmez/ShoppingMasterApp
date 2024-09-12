using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Money Balance { get; set; } = new Money(0, "USD");
        public ICollection<WalletTransaction> Transactions { get; set; }

        public void AddFunds(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive to add funds.");
            }
            Balance = new Money(Balance.Amount + amount, currency);
            Transactions.Add(new WalletTransaction(amount, TransactionType.Deposit, currency));
        }

        public void WithdrawFunds(decimal amount, string currency)
        {
            if (amount <= 0 || Balance.Amount < amount)
            {
                throw new ArgumentException("Insufficient balance to withdraw funds.");
            }
            Balance = new Money(Balance.Amount - amount, currency);
            Transactions.Add(new WalletTransaction(amount, TransactionType.Withdrawal, currency));
        }
    }
}
