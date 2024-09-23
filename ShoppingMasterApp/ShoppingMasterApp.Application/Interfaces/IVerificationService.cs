using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface IVerificationService
    {
        Task SendVerificationCodeAsync(BaseUser user, VerificationType verificationType);
        Task<bool> VerifyCodeAsync(BaseUser user, VerificationType verificationType, string code);
    }
}
