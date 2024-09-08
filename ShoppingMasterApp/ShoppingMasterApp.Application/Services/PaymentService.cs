using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ProcessPaymentAsync(ProcessPaymentCommand command)
        {
            var payment = new Payment
            {
                OrderId = command.OrderId,
                Amount = new Money(command.Amount, "USD"),
                PaymentDate = DateTime.UtcNow,
                PaymentDetails = new PaymentDetails
                {
                    CardNumber = command.CardNumber,
                    CardType = command.CardType,
                    ExpiryDate = command.ExpiryDate,
                    Cvv = command.Cvv
                }
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaymentDto> GetPaymentStatusAsync(int orderId)
        {
            var payment = await _paymentRepository.GetByIdAsync(orderId);
            if (payment == null)
                throw new KeyNotFoundException("Payment not found");

            return new PaymentDto
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount.Amount,
                PaymentMethod = payment.PaymentDetails.CardType,
                IsSuccessful = payment.IsSuccessful,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
