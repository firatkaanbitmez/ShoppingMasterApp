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
        await _paymentRepository.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> GetPaymentStatusAsync(int orderId)
    {
        return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
    }
}
