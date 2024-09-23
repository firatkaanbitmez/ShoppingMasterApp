using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly ISmsVerificationService _smsVerificationService;
        private readonly IEmailVerificationService _emailVerificationService;

        public VerificationService(ISmsVerificationService smsVerificationService,
                                   IEmailVerificationService emailVerificationService
                                )
        {
            _smsVerificationService = smsVerificationService;
            _emailVerificationService = emailVerificationService;
        }

        public async Task SendVerificationCodeAsync(BaseUser user, VerificationType verificationType)
        {
            string verificationCode = GenerateVerificationCode(6); // 6 karakterli alfanumerik kod
            DateTime expiryDate = DateTime.UtcNow.AddMinutes(5); // Kod 5 dakika geçerli

            if (verificationType == VerificationType.Email && !string.IsNullOrEmpty(user.Email.Value))
            {
                user.EmailVerificationCode = verificationCode;
                user.EmailVerificationExpiryDate = expiryDate;

                string emailMessage = $"Merhaba {user.FirstName}, email doğrulama kodunuz: {verificationCode}. Bu kod 5 dakika boyunca geçerlidir.";
                await _emailVerificationService.SendVerificationEmailAsync(user.Email.Value, "Email Doğrulama", emailMessage, $"<p>{emailMessage}</p>");
            }
            else if (verificationType == VerificationType.Sms && !string.IsNullOrEmpty(user.PhoneNumber.Number))
            {
                user.SmsVerificationCode = verificationCode;
                user.SmsVerificationExpiryDate = expiryDate;

                string fullPhoneNumber = user.PhoneNumber.GetFullNumber();
                await _smsVerificationService.SendVerificationSmsAsync(fullPhoneNumber, verificationCode);
            }

         
        }

        public async Task<bool> VerifyCodeAsync(BaseUser user, VerificationType verificationType, string code)
        {
            if (verificationType == VerificationType.Email)
            {
                if (user.EmailVerificationExpiryDate < DateTime.UtcNow)
                    throw new ArgumentException("Email doğrulama kodu süresi doldu.");

                if (user.EmailVerificationCode != code)
                    throw new ArgumentException("Geçersiz email doğrulama kodu.");

                user.IsEmailVerified = true;
            }
            else if (verificationType == VerificationType.Sms)
            {
                if (user.SmsVerificationExpiryDate < DateTime.UtcNow)
                    throw new ArgumentException("SMS doğrulama kodu süresi doldu.");

                var isValid = await _smsVerificationService.VerifyCodeAsync(user.PhoneNumber.GetFullNumber(), code);
                if (!isValid)
                    throw new ArgumentException("Geçersiz SMS doğrulama kodu.");

                user.IsSmsVerified = true;
            }
            
                

            return true;
        }

        private string GenerateVerificationCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
