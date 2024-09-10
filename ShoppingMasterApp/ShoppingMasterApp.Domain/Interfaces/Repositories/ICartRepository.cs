using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetByUserIdAsync(int userId);
        Task<CartItem> GetCartItemAsync(int cartId, int productId);
    }



}
