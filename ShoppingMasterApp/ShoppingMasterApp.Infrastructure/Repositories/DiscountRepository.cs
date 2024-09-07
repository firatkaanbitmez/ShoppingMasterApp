using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ShoppingMasterApp.Infrastructure.Repositories;

public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
{
    private readonly ApplicationDbContext _context;

    public DiscountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    // Get discount by code implementation
    public async Task<Discount> GetDiscountByCodeAsync(string code)
    {
        return await _context.Discounts.FirstOrDefaultAsync(d => d.Code == code);
    }
}
