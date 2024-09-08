using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.CQRS.Queries.User;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserCommand command);
        Task<UserDto> UpdateUserAsync(UpdateUserCommand command);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersByRoleAsync(Roles role);
    }
}
