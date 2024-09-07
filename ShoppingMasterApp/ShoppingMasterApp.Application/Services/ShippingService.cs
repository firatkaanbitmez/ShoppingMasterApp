using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class ShippingService : IShippingService
{
    private readonly IShippingRepository _shippingRepository;

    public ShippingService(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<Shipping> GetShippingByOrderIdAsync(int orderId)
    {
        return await _shippingRepository.GetShippingByOrderIdAsync(orderId);
    }

    public async Task CreateShippingAsync(Shipping shipping)
    {
        await _shippingRepository.AddAsync(shipping);
    }

    public async Task UpdateShippingAsync(Shipping shipping)
    {
        _shippingRepository.Update(shipping);
        await _shippingRepository.SaveChangesAsync();
    }
}
