using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IShippingRepository : IBaseRepository<Shipping>
    {
        Task<Shipping> GetShippingByOrderIdAsync(int orderId);
    }

}
