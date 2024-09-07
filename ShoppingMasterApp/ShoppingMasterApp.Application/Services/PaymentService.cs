using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> ProcessPaymentAsync(Payment payment)
    {
        if (payment.Amount.Amount <= 0) // Amount'ın decimal özelliğini kullanarak karşılaştırma yapıyoruz
            throw new ArgumentException("Payment amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(payment.PaymentDetails.CardNumber))
            throw new ArgumentException("Card number is required.");

        await _paymentRepository.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> GetPaymentStatusAsync(int orderId)
    {
        return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
    }
}
