using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly ISmsVerificationService _smsVerificationService;
        private readonly IEmailVerificationService _emailVerificationService;
        private readonly string _emailTemplateId;  // Declare the template ID field

        public VerificationService(ISmsVerificationService smsVerificationService,
                                   IEmailVerificationService emailVerificationService,
                                   IConfiguration configuration) // Inject the configuration
        {
            _smsVerificationService = smsVerificationService;
            _emailVerificationService = emailVerificationService;
            _emailTemplateId = configuration["SendGrid:TemplateId"]; // Fetch the template ID from appsettings
        }

        public async Task SendVerificationCodeAsync(BaseUser user, VerificationType verificationType)
        {
            if (!user.IsActive)
                throw new InvalidOperationException("User account is inactive. Please contact support.");

            string verificationCode = GenerateVerificationCode(6);
            DateTime expiryDate = DateTime.UtcNow.AddMinutes(5);
            TimeSpan? remainingTime = null;


            if (verificationType == VerificationType.Email && !string.IsNullOrEmpty(user.Email.Value))
            {
                if (user.EmailVerificationExpiryDate > DateTime.UtcNow)
                {
                    remainingTime = user.EmailVerificationExpiryDate.Value - DateTime.UtcNow;
                    throw new ArgumentException("You must wait before requesting a new email verification code.");

                }

                user.EmailVerificationCode = verificationCode;
                user.EmailVerificationExpiryDate = expiryDate;

                var dynamicData = new
                {
                    twilio_code = verificationCode,
                    user_name = user.FirstName ?? "Customer"
                };

                // Call the email verification service with the correct data, using the template ID from appsettings
                await _emailVerificationService.SendVerificationEmailUsingTemplateAsync(
                    user.Email.Value,
                    _emailTemplateId,  // Use the template ID from appsettings
                    dynamicData
                );
            }
            else if (verificationType == VerificationType.Sms && !string.IsNullOrEmpty(user.PhoneNumber.Number))
            {
                if (user.SmsVerificationExpiryDate > DateTime.UtcNow)
                {
                    remainingTime = user.SmsVerificationExpiryDate.Value - DateTime.UtcNow;
                    throw new ArgumentException("You must wait before requesting a new SMS verification code.");


                }

                user.SmsVerificationCode = verificationCode;
                user.SmsVerificationExpiryDate = expiryDate;

                string fullPhoneNumber = user.PhoneNumber.GetFullNumber();
                await _smsVerificationService.SendVerificationSmsAsync(fullPhoneNumber, verificationCode);
            }
        }

        public async Task<bool> VerifyCodeAsync(BaseUser user, VerificationType verificationType, string code)
        {
            if (!user.IsActive)
                throw new InvalidOperationException("User account is inactive. Please contact support.");

            if (verificationType == VerificationType.Email)
            {
                if (user.EmailVerificationExpiryDate < DateTime.UtcNow)
                    throw new ArgumentException("Email verification code expired.");

                if (user.EmailVerificationCode != code)
                    throw new ArgumentException("Invalid email verification code.");

                user.IsEmailVerified = true;
            }
            else if (verificationType == VerificationType.Sms)
            {
                if (user.SmsVerificationExpiryDate < DateTime.UtcNow)
                    throw new ArgumentException("SMS verification code expired.");

                var isValid = await _smsVerificationService.VerifyCodeAsync(user.PhoneNumber.GetFullNumber(), code);
                if (!isValid)
                    throw new ArgumentException("Invalid SMS verification code.");

                user.IsSmsVerified = true;
            }

            return true;
        }

        private string GenerateVerificationCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
