using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(ProcessPaymentCommand command);
        Task<PaymentDto> GetPaymentStatusAsync(int orderId);
    }


}
