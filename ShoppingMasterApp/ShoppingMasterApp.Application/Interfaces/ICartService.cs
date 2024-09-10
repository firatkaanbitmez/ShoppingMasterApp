using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task AddOrUpdateCartItemAsync(AddToCartCommand command); 
        Task RemoveCartItemAsync(RemoveFromCartCommand command); 
        Task ClearCartAsync(int userId); 
    }




}
