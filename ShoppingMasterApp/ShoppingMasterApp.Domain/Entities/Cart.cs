using ShoppingMasterApp.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(item => item.TotalPrice) + CalculateTaxes() - ApplyDiscount();
            }
        }

        private decimal CalculateTaxes()
        {
            // Vergi hesaplama mantığı
            return CartItems.Sum(item => item.TotalPrice * 0.18m); // %18 vergi örneği
        }

        private decimal ApplyDiscount()
        {
            // İndirim hesaplama mantığı (örneğin kupon kodu kullanımı)
            return 0; // İndirim yoksa 0 döner
        }
    }
}
