using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task<User> RegisterUserAsync(User user);
        Task UpdateUserProfileAsync(User user);
        Task CreateUserAsync(CreateUserCommand command);  
        Task UpdateUserAsync(UpdateUserCommand command);  
        Task DeleteUserAsync(int id);                     
    }
}
