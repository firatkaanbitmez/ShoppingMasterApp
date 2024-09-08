using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }  // Value object for Email
        public string PasswordHash { get; set; }
        public Roles Roles { get; set; }  // Enum for Roles (Admin, Customer, Vendor)
        public Address Address { get; set; }  // ValueObject for Address
        public ICollection<Order> Orders { get; set; }
    }
}
