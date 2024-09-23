using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Microsoft.Extensions.Configuration;
using ShoppingMasterApp.Application.Interfaces;

namespace ShoppingMasterApp.Application.Services
{
    public class SmsVerificationService : ISmsVerificationService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _serviceSid;

        public SmsVerificationService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _serviceSid = configuration["Twilio:ServiceSid"];
        }

        // Özel doğrulama kodu gönderme metodu
        public async Task SendVerificationSmsAsync(string toPhoneNumber, string customCode)
        {
            TwilioClient.Init(_accountSid, _authToken);

            // Telefon numarasını uluslararası formata çevir
            var formattedPhoneNumber = $"+{toPhoneNumber.TrimStart('+')}";

            // Twilio API'yi kullanarak SMS gönderme (customCode parametresini kullanıyoruz)
            var verification = await VerificationResource.CreateAsync(
                customCode: customCode, // Özel kodu gönder
                channel: "sms",
                to: formattedPhoneNumber,
                pathServiceSid: _serviceSid
            );

            if (verification.Status != "pending")
            {
                throw new Exception($"SMS gönderimi başarısız oldu: {verification.Status}");
            }
        }

        // Twilio ile doğrulama kodunu kontrol etme
        public async Task<bool> VerifyCodeAsync(string toPhoneNumber, string code)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: toPhoneNumber,
                code: code,
                pathServiceSid: _serviceSid
            );

            return verificationCheck.Status == "approved";
        }
    }
}
