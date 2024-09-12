namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public string MaskedCardNumber
        {
            get => new string('X', CardNumber.Length - 4) + CardNumber[^4..];
        }
    }

}
