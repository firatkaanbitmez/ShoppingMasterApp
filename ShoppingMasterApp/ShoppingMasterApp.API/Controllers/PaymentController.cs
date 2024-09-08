using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            if (!ModelState.IsValid)
                return ApiValidationError(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            await _paymentService.ProcessPaymentAsync(command);
            return ApiResponse("Payment processed successfully");
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetPaymentStatus(int orderId)
        {
            var result = await _paymentService.GetPaymentStatusAsync(orderId);
            return ApiResponse(result);
        }
    }
}
