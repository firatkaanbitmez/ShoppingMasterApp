using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.CQRS.Queries.User;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return ApiResponse(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("User created successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("User updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return ApiResponse("User deleted successfully");
        }
    }
}
