using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Customer;
using ShoppingMasterApp.Application.CQRS.Queries.Customer;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Customer registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerCommand command)
        {
            var token = await _mediator.Send(command);
            return ApiResponse(token, "Login successful.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Customer created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id; // Ensure the ID is passed
            await _mediator.Send(command);
            return ApiResponse("Customer updated successfully");
        }

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordCommand command)
        {
            command.CustomerId = id;
            await _mediator.Send(command);
            return ApiResponse("Password updated successfully.");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCustomerInfo()
        {
            try
            {
                var userId = GetUserIdFromToken();
                var customer = await _mediator.Send(new GetCustomerByIdQuery { Id = userId });
                return ApiResponse(customer);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return ApiResponse("Customer deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return ApiResponse(result);
        }
    }
}
