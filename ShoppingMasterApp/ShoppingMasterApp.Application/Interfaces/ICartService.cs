using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(AddToCartCommand command);
        Task RemoveFromCartAsync(RemoveFromCartCommand command);
        Task ClearCartAsync(ClearCartCommand command);
        Task<CartDto> GetCartByUserIdAsync(int userId);
    }


}
