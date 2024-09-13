using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IShippingRepository : IGenericRepository<Shipping>
    {
        Task<Shipping> GetShippingByOrderIdAsync(int orderId); 
    }
}
