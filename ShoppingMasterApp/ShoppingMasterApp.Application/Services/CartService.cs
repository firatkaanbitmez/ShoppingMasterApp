using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using System.Linq;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddToCartAsync(AddToCartCommand command)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(command.UserId);
        if (cart == null)
        {
            cart = new Cart { UserId = command.UserId };
            await _cartRepository.AddAsync(cart);
        }

        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (cartItem != null)
        {
            cartItem.Quantity += command.Quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = command.ProductId,
                Quantity = command.Quantity
            });
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(RemoveFromCartCommand command)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(command.UserId);
        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (cartItem != null)
        {
            cart.CartItems.Remove(cartItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(ClearCartCommand command)
    {
        var cart = await _cartRepository.GetByIdAsync(command.CartId);
        if (cart != null)
        {
            cart.CartItems.Clear();
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<CartDto> GetCartByUserIdAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return null;

        return _mapper.Map<CartDto>(cart);
    }
}
