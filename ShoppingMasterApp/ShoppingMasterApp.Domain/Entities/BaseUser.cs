using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System;

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
        public bool IsEmailVerified { get; set; } = false;
        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationExpiryDate { get; set; }

        // SMS Verification
        public bool IsSmsVerified { get; set; } = false;
        public string? SmsVerificationCode { get; set; }
        public DateTime? SmsVerificationExpiryDate { get; set; }

        // Attempt limits
        public int FailedEmailAttempts { get; set; } = 0;
        public int FailedSmsAttempts { get; set; } = 0;
        public DateTime? LastEmailAttemptTime { get; set; }
        public DateTime? LastSmsAttemptTime { get; set; }

        // Maximum number of allowed attempts before locking
        private const int MaxFailedAttempts = 5;
    

        protected BaseUser(Roles role)
        {
            Roles = role;
        }

        public void CheckAccountLockout()
        {
            if (FailedEmailAttempts >= MaxFailedAttempts || FailedSmsAttempts >= MaxFailedAttempts)
            {
                IsActive = false; // Deactivate account if too many failed attempts
            }
        }



    }
}
