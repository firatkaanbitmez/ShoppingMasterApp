using System.Security.Cryptography;
using System.Text;

namespace ShoppingMasterApp.Domain.ValueObjects
{
    public class PaymentDetails
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string EncryptedCardNumber { get; private set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public PaymentDetails(string cardNumber)
        {
            CardNumber = cardNumber;  // Düz CardNumber kaydediyoruz
            EncryptedCardNumber = Encrypt(cardNumber);  // Şifrelenmiş hali saklanıyor
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
