using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }
}
