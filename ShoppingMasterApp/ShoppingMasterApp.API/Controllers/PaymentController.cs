using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? ApiResponse("Payment processed successfully.") : ApiError("Payment processing failed.");
        }
    }
}
