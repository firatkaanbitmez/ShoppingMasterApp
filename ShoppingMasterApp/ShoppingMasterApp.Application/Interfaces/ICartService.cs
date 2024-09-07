using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<Cart> GetUserCartAsync(int userId);
        Task AddToCartAsync(AddToCartCommand command);
        Task UpdateCartItemAsync(UpdateCartItemCommand command); 
        Task RemoveFromCartAsync(RemoveFromCartCommand command); 
        Task ClearCartAsync(int cartId);
    }

}
