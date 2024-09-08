using ShoppingMasterApp.Application.CQRS.Commands.Discount;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IDiscountService
    {
        Task ApplyDiscountAsync(ApplyDiscountCommand command);
        Task CreateDiscountAsync(CreateDiscountCommand command);
        Task RemoveDiscountAsync(RemoveDiscountCommand command);
    }


}