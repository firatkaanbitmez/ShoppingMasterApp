using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.CQRS.Queries.User;
using ShoppingMasterApp.Domain.Enums;

namespace ShoppingMasterApp.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, UpdateUserProfileCommand command)
        {
            if (id != command.Id) return ApiError("Invalid User ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUsersByRole(Roles roles)
        {
            var result = await _mediator.Send(new GetUsersByRoleQuery { Roles = roles });
            return ApiResponse(result);
        }
    }
}
