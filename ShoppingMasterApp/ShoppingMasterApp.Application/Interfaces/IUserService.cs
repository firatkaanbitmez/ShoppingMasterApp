using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(User user);
        Task UpdateUserProfileAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    }
}
