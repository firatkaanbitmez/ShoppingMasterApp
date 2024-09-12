using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
