using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);
    }
}
