using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();  // Tüm ürünleri döner

    }
}
