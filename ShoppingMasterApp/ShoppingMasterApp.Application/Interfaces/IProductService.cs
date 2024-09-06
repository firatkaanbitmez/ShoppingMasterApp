using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();  // Async versiyonu
        Task<Product> GetProductByIdAsync(int id);  // Async versiyonu
        Task<Product> CreateProductAsync(CreateProductCommand command);  // Async versiyonu
        Task<Product> UpdateProductAsync(UpdateProductCommand command);  // Async versiyonu
        Task DeleteProductAsync(int id);  // Async versiyonu
    }
}
