using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class ShippingRepository : GenericRepository<Shipping>, IShippingRepository
    {
        private readonly ApplicationDbContext _context;

        public ShippingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Shipping> GetShippingByOrderIdAsync(int orderId)
        {
            return await _context.Shippings
                .Include(s => s.ShippingAddress) 
                .FirstOrDefaultAsync(s => s.OrderId == orderId);
        }
    }
}
