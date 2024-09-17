using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Customer;
using ShoppingMasterApp.Application.CQRS.Queries.Customer;
using ShoppingMasterApp.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT token gerektiren tüm işlemler için
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Sadece Customer kaydı için yetkilendirme gerekmez
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Customer registered successfully.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerCommand command)
        {
            var token = await _mediator.Send(command);
            return ApiResponse(token, "Login successful.");
        }

        // Sadece Admin ve Manager rolündeki kullanıcılar müşteri oluşturabilir
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Customer created successfully.");
        }

        [Authorize(Roles = "Admin,Manager,Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
            var userId = GetUserIdFromToken();

            // Eğer müşteri rolündeyse ve kendi ID'si dışında bir kullanıcıyı güncellemeye çalışıyorsa hata ver
            if (User.IsInRole("Customer") && userId != id)
            {
                return ApiError("Başka bir müşterinin bilgilerini güncelleme yetkiniz yok.", 403);
            }

            command.Id = id;
            await _mediator.Send(command);
            return ApiResponse("Customer updated successfullyi.");
        }


        [Authorize(Roles = "Admin,Manager,Customer")]
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordCommand command)
        {
            var userId = GetUserIdFromToken();

            // Eğer müşteri rolündeyse ve başka bir kullanıcının şifresini değiştirmeye çalışıyorsa hata ver
            if (User.IsInRole("Customer") && userId != id)
            {
                return ApiError("Başka bir müşterinin şifresini değiştirme yetkiniz yok.", 403);
            }

            command.CustomerId = id;
            await _mediator.Send(command);
            return ApiResponse("Şifre başarıyla güncellendi.");
        }

        [Authorize(Roles = "Customer")]
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


        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return ApiResponse("Customer deleted successfully.");
        }

        [Authorize(Roles = "Admin,Manager,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var userId = GetUserIdFromToken();

            // Eğer müşteri rolündeyse ve kendi ID'si dışında bir veri sorguluyorsa, hata ver
            if (User.IsInRole("Customer") && userId != id)
            {
                return ApiError("Başka bir müşterinin bilgilerine erişim yetkiniz yok.", 403);
            }

            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            return ApiResponse(result);
        }


        // Sadece Admin ve Manager tüm müşteri listesine erişebilir
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return ApiResponse(result);
        }
    }
}
