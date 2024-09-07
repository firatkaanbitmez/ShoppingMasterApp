using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Models;
namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetPagedProductsAsync(PagedQuery query);
    }

}
