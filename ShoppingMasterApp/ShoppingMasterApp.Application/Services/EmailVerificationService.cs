using ShoppingMasterApp.Application.Interfaces;
using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace ShoppingMasterApp.Application.Services
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly string _sendGridApiKey;
        private readonly string _fromEmail;

        public EmailVerificationService(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ApiKey"];
            _fromEmail = configuration["SendGrid:FromEmail"];
        }

        public async Task SendVerificationEmailAsync(string toEmail, string subject, string plainTextMessage, string htmlMessage)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_fromEmail, "ShoppingMaster App");
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Email gönderimi başarısız oldu: {response.StatusCode}");
            }
        }
    }
}
