using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(CreateOrderCommand command);
        Task UpdateOrderAsync(UpdateOrderCommand command);
        Task DeleteOrderAsync(int id);
    }
}
