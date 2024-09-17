using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> GetAdminByEmailAsync(string email);
    }
}
