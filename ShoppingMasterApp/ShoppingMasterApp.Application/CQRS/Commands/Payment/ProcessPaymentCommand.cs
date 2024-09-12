using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
namespace ShoppingMasterApp.Application.CQRS.Commands.Payment
{

    public class ProcessPaymentCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }

        public class Handler : IRequestHandler<ProcessPaymentCommand, bool>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IPaymentRepository _paymentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
            {
                _orderRepository = orderRepository;
                _paymentRepository = paymentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                if (order == null || order.TotalAmount.Amount != request.Amount)
                {
                    throw new KeyNotFoundException("Order not found or amount mismatch");
                }

                var payment = new ShoppingMasterApp.Domain.Entities.Payment
                {
                    OrderId = request.OrderId,
                    PaymentDate = DateTime.UtcNow,
                    Amount = new Money(request.Amount, "USD"),
                    PaymentDetails = new PaymentDetails
                    {
                        CardType = request.CardType,
                        CardNumber = request.CardNumber,
                        ExpiryDate = request.ExpiryDate,
                        Cvv = request.Cvv
                    },
                    IsSuccessful = ProcessPaymentGateway(request) // Payment gateway logic
                };

                order.Payment = payment;

                if (payment.IsSuccessful)
                {
                    order.Shipping.UpdateStatus(ShippingStatus.Preparing);
                }

                _paymentRepository.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync();

                return payment.IsSuccessful;
            }

            private bool ProcessPaymentGateway(ProcessPaymentCommand request)
            {
                // Placeholder for calling a payment gateway API
                return true;
            }
        }
    }

}
