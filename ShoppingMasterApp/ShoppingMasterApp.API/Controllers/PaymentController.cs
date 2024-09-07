using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
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
            await _paymentService.ProcessPaymentAsync(new Domain.Entities.Payment
            {
                OrderId = command.OrderId,
                Amount = new Domain.ValueObjects.Money(command.Amount, "USD"),
                PaymentDetails = new Domain.ValueObjects.PaymentDetails(command.CardType, command.CardNumber, command.ExpiryDate)
            });

            return ApiSuccess<object>(null, "Payment processed successfully");
        }

        [HttpGet("status/{orderId}")]
        public async Task<IActionResult> GetPaymentStatus(int orderId)
        {
            var payment = await _paymentService.GetPaymentStatusAsync(orderId);
            var amount = payment.Amount.Amount;

            return ApiSuccess(new { Amount = amount }, "Payment status retrieved successfully");
        }
    }
}
