using System.Security.Cryptography;
using System.Text;
using System;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public string CardType { get; private set; }
        public string CardNumber { get; private set; }
        public string EncryptedCardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string Cvv { get; private set; }

        // Constructor
        public PaymentDetails(string cardType, string cardNumber, string expiryDate, string cvv)
        {
            CardType = cardType;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Cvv = cvv;
            EncryptedCardNumber = Encrypt(cardNumber);
        }

        // Kart numarasını şifreleme
        private string Encrypt(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }
    }

}
