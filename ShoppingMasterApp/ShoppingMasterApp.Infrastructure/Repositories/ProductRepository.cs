using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.Models;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetPagedProductsAsync(PagedQuery query)
    {
        return await _context.Products
                             .Where(p => !p.IsDeleted)
                             .Skip((query.PageNumber - 1) * query.PageSize)
                             .Take(query.PageSize)
                             .ToListAsync();
    }
}
