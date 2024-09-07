using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<Discount> GetDiscountByCodeAsync(string code);
    }

}
