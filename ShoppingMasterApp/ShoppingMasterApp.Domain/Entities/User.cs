using ShoppingMasterApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // Müşteri, Admin, Satıcı
        public ICollection<Order> Orders { get; set; }  // Kullanıcı sipariş ilişkisi
    }

}
