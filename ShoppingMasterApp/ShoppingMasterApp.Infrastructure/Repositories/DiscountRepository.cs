using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ShoppingMasterApp.Infrastructure.Repositories;

public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
{
    private readonly ApplicationDbContext _context;

    public DiscountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

   
}
