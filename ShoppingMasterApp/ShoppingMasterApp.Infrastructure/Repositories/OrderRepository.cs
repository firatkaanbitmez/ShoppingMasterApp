using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) 
        {
        }
    }
}
