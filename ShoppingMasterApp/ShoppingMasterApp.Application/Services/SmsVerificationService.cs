using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;
using ShoppingMasterApp.Application.Interfaces;

namespace ShoppingMasterApp.Application.Services
{
    public class SmsVerificationService : ISmsVerificationService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromPhoneNumber;

        public SmsVerificationService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _fromPhoneNumber = configuration["Twilio:FromPhoneNumber"];
        }

        public async Task SendVerificationSmsAsync(string toPhoneNumber, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber(_fromPhoneNumber),
                Body = message
            };

            var msg = await MessageResource.CreateAsync(messageOptions);
            if (msg.ErrorCode != null)
            {
                throw new Exception($"SMS gönderimi başarısız oldu: {msg.ErrorMessage}");
            }
        }
    }
}
