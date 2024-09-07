using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    }
}
