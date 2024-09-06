using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(CreateProductCommand command);
        Task<Product> GetProductByIdAsync(int id);
        Task DeleteProductAsync(int id);
    }
}
