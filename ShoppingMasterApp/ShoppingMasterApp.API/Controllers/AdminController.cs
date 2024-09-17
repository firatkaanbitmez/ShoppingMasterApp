using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Admin;
using ShoppingMasterApp.Application.CQRS.Queries.Admin;
using ShoppingMasterApp.Application.Interfaces;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator, ITokenService tokenService) : base(tokenService)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdminCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Admin registered successfully.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand command)
        {
            var token = await _mediator.Send(command);
            return ApiResponse(token, "Login successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateAdminCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return ApiResponse("Admin updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            await _mediator.Send(new DeleteAdminCommand { Id = id });
            return ApiResponse("Admin deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _mediator.Send(new GetAdminByIdQuery { Id = id });
            return ApiResponse(admin);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _mediator.Send(new GetAllAdminsQuery());
            return ApiResponse(admins);
        }
    }
}
