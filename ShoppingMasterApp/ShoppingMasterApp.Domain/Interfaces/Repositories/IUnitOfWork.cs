using ShoppingMasterApp.Domain.Interfaces.Repositories;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }

        Task<int> SaveChangesAsync();
    }

}
