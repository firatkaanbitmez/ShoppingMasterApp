namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public string CardType { get; }
        public string CardNumber { get; }
        public string ExpiryDate { get; }

        public PaymentDetails(string cardType, string cardNumber, string expiryDate)
        {
            if (string.IsNullOrWhiteSpace(cardType) || string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(expiryDate))
                throw new ArgumentException("All fields must be provided");

            CardType = cardType;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
        }
    }

}
