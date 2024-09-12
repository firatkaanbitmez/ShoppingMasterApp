using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System;

namespace ShoppingMasterApp.Domain.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Currency { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public WalletTransaction(decimal amount, TransactionType transactionType, string currency)
        {
            Amount = amount;
            TransactionType = transactionType;
            Currency = currency;
        }
    }
}
