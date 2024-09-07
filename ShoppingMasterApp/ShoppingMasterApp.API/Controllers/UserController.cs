using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return ApiNotFound("User not found");
            }

            return ApiSuccess(user, "User retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ApiValidationError(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
            }

            await _userService.CreateUserAsync(command);
            return ApiSuccess<object>(null, "User created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ApiValidationError(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return ApiNotFound("User not found");
            }

            command.Id = id; // Route'dan gelen ID'yi komuta aktar

            await _userService.UpdateUserAsync(command);
            return ApiSuccess<object>(null, "User updated successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return ApiNotFound("User not found");
            }

            await _userService.DeleteUserAsync(id);
            return ApiSuccess<object>(null, "User deleted successfully");
        }
    }
}
