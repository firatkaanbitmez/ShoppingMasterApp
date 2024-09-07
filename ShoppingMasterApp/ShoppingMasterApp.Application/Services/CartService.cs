using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

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

    public async Task AddToCartAsync(Cart cart)
    {
        await _cartRepository.AddAsync(cart);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(Cart cart)
    {
        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(Cart cart)
    {
        _cartRepository.Delete(cart);
        await _unitOfWork.SaveChangesAsync();
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
