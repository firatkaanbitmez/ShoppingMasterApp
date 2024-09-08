using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductCommand command);
        Task UpdateProductAsync(UpdateProductCommand command);
        Task DeleteProductAsync(DeleteProductCommand command);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }


}
