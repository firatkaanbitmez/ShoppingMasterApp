using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<OrderDto> GetOrderById(int id);
        Task<OrderDto> CreateOrder(CreateOrderCommand command);
        Task<OrderDto> UpdateOrder(UpdateOrderCommand command);
        Task<bool> DeleteOrder(int id);
    }
}
