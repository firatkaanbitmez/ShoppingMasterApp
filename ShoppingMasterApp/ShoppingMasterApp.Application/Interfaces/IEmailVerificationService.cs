using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface IEmailVerificationService
    {
        Task SendVerificationEmailAsync(string recipient, string subject, string plainTextMessage, string htmlMessage);
    }
}
