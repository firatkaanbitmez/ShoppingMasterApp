using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductCommand command);
        Task UpdateProductAsync(UpdateProductCommand command);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetPagedProductsAsync(PagedQuery query);  // Modify the return type here
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task ChangeProductStockAsync(ChangeProductStockCommand command);
    }
}
