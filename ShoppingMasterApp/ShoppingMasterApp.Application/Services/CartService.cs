using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Cart> GetUserCartAsync(int userId)
    {
        return await _cartRepository.GetUserCartAsync(userId);
    }

    public async Task AddToCartAsync(AddToCartCommand command)
    {
        var cartItem = new CartItem
        {
            ProductId = command.ProductId,
            Quantity = command.Quantity,
            CartId = command.UserId
        };
        await _cartRepository.AddAsync(new Cart
        {
            UserId = command.UserId,
            CartItems = new List<CartItem> { cartItem }
        });
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(UpdateCartItemCommand command)
    {
        var cart = await _cartRepository.GetByIdAsync(command.UserId);
        if (cart != null)
        {
            var item = cart.CartItems.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (item != null)
            {
                item.Quantity = command.Quantity;
            }
            _cartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task RemoveFromCartAsync(RemoveFromCartCommand command)
    {
        var cart = await _cartRepository.GetUserCartAsync(command.UserId);
        if (cart != null)
        {
            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == command.ProductId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

    public async Task ClearCartAsync(int cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        if (cart != null)
        {
            cart.CartItems.Clear();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
