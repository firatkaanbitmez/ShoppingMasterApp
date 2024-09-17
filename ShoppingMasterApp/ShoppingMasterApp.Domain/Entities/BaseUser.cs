using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public abstract class BaseUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }
        public string PasswordHash { get; set; }
        public Roles Roles { get; set; }
        public bool IsVerified { get; set; }  
        public string VerificationCode { get; set; }  

        protected BaseUser(Roles role)
        {
            Roles = role;
        }
    }
}
