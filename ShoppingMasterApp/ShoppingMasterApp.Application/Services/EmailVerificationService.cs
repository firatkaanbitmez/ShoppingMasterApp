using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using ShoppingMasterApp.Application.Interfaces;
using System.Threading.Tasks;

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

        public async Task SendVerificationEmailUsingTemplateAsync(string toEmail, string templateId, object dynamicData)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_fromEmail, "ShoppingMaster App");
            var to = new EmailAddress(toEmail);

            var subject = "ShoppingMaster Verification";  // Set the subject explicitly

            var msg = new SendGridMessage
            {
                From = from,
                TemplateId = templateId,
                Subject = subject  // Forcing the subject here
            };

            // Add recipient and template data
            msg.AddTo(to);
            msg.SetTemplateData(dynamicData);

            // Send the email
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email: {response.StatusCode}");
            }
        }

    }
}
