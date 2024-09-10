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
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CartDto> GetCartByUserIdAsync(int userId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _cartRepository.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();
        }
        return _mapper.Map<CartDto>(cart);
    }

    public async Task AddOrUpdateCartItemAsync(AddToCartCommand command)
    {
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId);
        if (cart == null)
        {
            cart = new Cart { UserId = command.UserId };
            await _cartRepository.AddAsync(cart);  // Yeni sepeti ekle
            await _unitOfWork.SaveChangesAsync();  // Veritabanına kaydet
        }

        var product = await _productRepository.GetByIdAsync(command.ProductId);
        if (product == null || product.Stock < command.Quantity)
        {
            throw new Exception("Product is out of stock.");
        }

        // Sepete ürün ekle veya güncelle
        cart.AddOrUpdateItem(product, command.Quantity);

        // Sepet ve ürünleri kaydet
        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync();  // Veritabanına tüm değişiklikleri kaydet
    }


    public async Task RemoveCartItemAsync(RemoveFromCartCommand command)
    {
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId);
        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == command.ProductId);
        if (cartItem != null)
        {
            cart.RemoveItem(command.ProductId);  // Ürünü sepetten kaldır, CalculateTotalPrice otomatik çağrılır

            _cartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();  // Değişiklikleri kaydet
        }
        else
        {
            throw new Exception("Cart item not found.");
        }
    }


    public async Task ClearCartAsync(int userId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);
        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        cart.Clear();  // Sepeti temizle, CalculateTotalPrice otomatik çağrılır
        _cartRepository.Update(cart);  // Değişiklikleri kaydet
        await _unitOfWork.SaveChangesAsync();  // Veritabanına kaydet
    }


}
