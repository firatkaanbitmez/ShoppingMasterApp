using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Roles { get; set; }  // Admin, Customer, Vendor
        public ICollection<Order> Orders { get; set; }
        public Address Address { get; set; }  // ValueObject for address
    }
}
