using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Infrastructure.Repositories;

public class ShippingRepository : BaseRepository<Shipping>, IShippingRepository
{
    private readonly ApplicationDbContext _context;

    public ShippingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
 


}
