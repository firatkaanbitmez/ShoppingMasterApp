using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOrderAsync(CreateOrderCommand command)
    {
        var order = new Order
        {
            UserId = command.UserId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = new Money(command.TotalAmount, "USD") // Properly constructing Money
        };
        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(UpdateOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.Id);
        if (order == null)
        {
            throw new KeyNotFoundException("Order not found");
        }

        order.TotalAmount = new Money(command.TotalAmount, "USD"); // Properly constructing Money
        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteOrderAsync(DeleteOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.Id);
        if (order != null)
        {
            _orderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
