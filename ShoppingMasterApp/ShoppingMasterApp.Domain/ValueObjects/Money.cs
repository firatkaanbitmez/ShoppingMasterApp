namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        // Overload the multiplication operator to allow multiplying Money by an integer
        public static Money operator *(Money money, int quantity)
        {
            return new Money(money.Amount * quantity, money.Currency);
        }

        // Override ToString for easier display
        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
