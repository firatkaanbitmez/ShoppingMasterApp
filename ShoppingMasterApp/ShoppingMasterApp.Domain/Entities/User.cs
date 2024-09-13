using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;

namespace ShoppingMasterApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; } 
        public string PasswordHash { get; set; }
        public Roles Roles { get; set; } 
        public Address Address { get; set; }  
        public ICollection<Order> Orders { get; set; }
    }
}
