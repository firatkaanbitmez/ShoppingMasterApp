namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be empty");

            Amount = amount;
            Currency = currency;
        }
    }



}
