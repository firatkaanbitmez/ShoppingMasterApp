namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public string CardType { get; }
        public string CardNumber { get; }
        public string ExpiryDate { get; }

        public PaymentDetails(string cardType, string cardNumber, string expiryDate)
        {
            CardType = cardType;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
        }

        // Custom logic for masking card numbers
    }
}
