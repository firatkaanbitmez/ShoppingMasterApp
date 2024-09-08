using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task CommitAsync();
    }


}
