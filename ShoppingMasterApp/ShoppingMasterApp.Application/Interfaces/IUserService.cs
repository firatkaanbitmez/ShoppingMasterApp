using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserCommand command);
        Task UpdateUserAsync(UpdateUserCommand command);
        Task DeleteUserAsync(DeleteUserCommand command);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string role);
    }
}
