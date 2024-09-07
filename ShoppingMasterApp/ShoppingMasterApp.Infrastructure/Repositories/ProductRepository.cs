using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
       
    }
}
