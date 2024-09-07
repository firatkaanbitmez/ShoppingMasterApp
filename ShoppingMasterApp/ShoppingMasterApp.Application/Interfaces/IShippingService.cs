using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IShippingService
    {
        Task<Shipping> GetShippingByOrderIdAsync(int orderId);
        Task CreateShippingAsync(Shipping shipping);
        Task UpdateShippingAsync(Shipping shipping);
    }
}
