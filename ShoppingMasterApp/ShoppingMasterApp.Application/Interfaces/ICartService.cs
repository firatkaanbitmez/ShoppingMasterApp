using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<Cart> GetUserCartAsync(int userId);
        Task AddToCartAsync(Cart cart);
        Task UpdateCartItemAsync(Cart cart);
        Task RemoveFromCartAsync(Cart cart);
        Task ClearCartAsync(int cartId);
    }
}
