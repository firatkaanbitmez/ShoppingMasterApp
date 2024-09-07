using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.Interfaces.Services;

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
            return ApiResponse(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _userService.CreateUserAsync(command);
            return ApiResponse(message: "User created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var user = await _userService.GetUserByIdAsync(command.Id); // ID body'den alınıyor
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            await _userService.UpdateUserAsync(command);
            return Ok(new { message = "User updated successfully" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return ApiResponse(message: "User deleted successfully");
        }
    }
}
