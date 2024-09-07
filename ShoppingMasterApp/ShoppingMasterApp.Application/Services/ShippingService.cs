using ShoppingMasterApp.Application.CQRS.Commands.Shipping;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;

public class ShippingService : IShippingService
{
    private readonly IShippingRepository _shippingRepository;

    public ShippingService(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

  


}
