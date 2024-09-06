using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrder(CreateOrderCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            var order = new Order
            {
               
            };
                       
            await _orderRepository.AddAsync(order);

            var orderDto = _mapper.Map<OrderDto>(order);
           
            return orderDto;
        }

        public async Task<OrderDto> UpdateOrder(UpdateOrderCommand command)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            var product = await _productRepository.GetByIdAsync(command.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }


            await _orderRepository.UpdateAsync(order);

            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
                return true;
            }
            return false;
        }

      
    }
}
