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
        public PhoneNumber PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public Roles Roles { get; set; }

        // Email Verification
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationCode { get; set; }
        public DateTime EmailVerificationExpiryDate { get; set; }

        // SMS Verification
        public bool IsSmsVerified { get; set; }
        public string SmsVerificationCode { get; set; }
        public DateTime SmsVerificationExpiryDate { get; set; }


        protected BaseUser(Roles role)
        {
            Roles = role;
        }
    }
}
