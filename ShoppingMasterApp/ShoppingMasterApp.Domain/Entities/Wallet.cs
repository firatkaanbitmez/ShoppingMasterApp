using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Money Balance { get; private set; }

        public Wallet(Money initialBalance)
        {
            Balance = initialBalance;
        }

        public void AddFunds(Money amount)
        {
            if (amount.Amount <= 0) throw new InvalidOperationException("Amount must be positive.");
            Balance = new Money(Balance.Amount + amount.Amount, Balance.Currency);
        }

        public void DeductFunds(Money amount)
        {
            if (amount.Amount > Balance.Amount) throw new InvalidOperationException("Insufficient funds.");
            Balance = new Money(Balance.Amount - amount.Amount, Balance.Currency);
        }
    }
}
