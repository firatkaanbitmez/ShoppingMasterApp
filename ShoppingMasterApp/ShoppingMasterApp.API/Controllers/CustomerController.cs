using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands;
using ShoppingMasterApp.Application.CQRS.Commands.Customer;
using ShoppingMasterApp.Application.CQRS.Queries.Customer;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Enums;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator, ITokenService tokenService)
            : base(tokenService)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return Ok("Register successful, Please Email and PhoneNumber verify.");
        }


        [AllowAnonymous]
        [HttpPost("send-verification")]
        public async Task<IActionResult> SendVerification([FromBody] SendVerificationCodeCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Verification code sent.");
        }

        [AllowAnonymous]
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Verification successful.");
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerCommand command)
        {
            var token = await _mediator.Send(command);
            return ApiResponse(token, "Login successful.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Customer created successfully.");
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
            ValidateCustomerAccess(id);
            command.Id = id;
            await _mediator.Send(command);
            return ApiResponse("Customer updated successfully.");
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordCommand command)
        {
            ValidateCustomerAccess(id);
            command.CustomerId = id;
            await _mediator.Send(command);
            return ApiResponse("Password changed successfully.");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("me")]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var userId = GetUserIdFromToken();
            var customer = await _mediator.Send(new GetCustomerByIdQuery { Id = userId });
            return ApiResponse(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return ApiResponse("Customer deleted successfully.");
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            ValidateCustomerAccess(id);
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return ApiResponse(result);
        }

        private void ValidateCustomerAccess(int id)
        {
            var userId = GetUserIdFromToken();
            if (User.IsInRole("Customer") && userId != id)
            {
                throw new UnauthorizedAccessException("You do not have permission to access other customer's data.");
            }
        }
    }
}
