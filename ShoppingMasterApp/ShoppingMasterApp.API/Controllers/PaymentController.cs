using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.CQRS.Queries.Payment;

namespace ShoppingMasterApp.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(ProcessPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpGet("status/{orderId}")]
        public async Task<IActionResult> GetPaymentStatus(int orderId)
        {
            var result = await _mediator.Send(new GetPaymentStatusQuery { OrderId = orderId });
            return ApiResponse(result);
        }
    }
}
