using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface IEmailVerificationService
    {
        Task SendVerificationEmailUsingTemplateAsync(string toEmail, string templateId, object dynamicData);
    }
}
