using ShoppingMasterApp.Domain.Entities;
using System.Security.Claims;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface ITokenService
    {
        int GetUserIdFromToken(ClaimsPrincipal user); // Token'dan kullanıcı ID'sini çekme
        string GenerateToken(BaseUser user); // Token oluşturma
    }
}
