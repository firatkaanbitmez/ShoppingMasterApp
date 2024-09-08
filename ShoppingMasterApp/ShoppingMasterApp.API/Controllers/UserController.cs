using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.CQRS.Queries.User;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Enums;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _userService.CreateUserAsync(command);
            return ApiSuccess(result, "User created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id) return ApiValidationError(new[] { "User ID mismatch" });

            var result = await _userService.UpdateUserAsync(command);
            return ApiSuccess(result, "User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return ApiSuccess<object>(null, "User deleted successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return ApiSuccess(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return ApiSuccess(result);
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUsersByRole(Roles role)
        {
            var result = await _userService.GetUsersByRoleAsync(role);
            return ApiSuccess(result);
        }
    }
}
