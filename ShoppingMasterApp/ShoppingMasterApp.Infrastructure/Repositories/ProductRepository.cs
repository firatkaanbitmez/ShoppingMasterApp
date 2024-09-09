using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Application.CQRS.Queries.Product;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
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

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category)
                                      .Include(p => p.ProductDetails)  // İlişkili entity'leri de dahil ediyoruz
                                      .ToListAsync();  // Tüm ürünleri getirir
    }
}
