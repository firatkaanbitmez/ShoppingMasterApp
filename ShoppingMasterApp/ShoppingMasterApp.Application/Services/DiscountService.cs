using ShoppingMasterApp.Application.CQRS.Commands.Discount;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DiscountService(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ApplyDiscountAsync(ApplyDiscountCommand command)
    {
        // Apply discount logic
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateDiscountAsync(CreateDiscountCommand command)
    {
        var discount = new Discount { Amount = command.Amount };
        await _discountRepository.AddAsync(discount);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveDiscountAsync(RemoveDiscountCommand command)
    {
        var discount = await _discountRepository.GetByIdAsync(command.Id);
        if (discount != null)
        {
            _discountRepository.Delete(discount);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
