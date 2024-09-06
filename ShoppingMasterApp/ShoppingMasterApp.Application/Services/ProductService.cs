using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Exceptions;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

namespace ShoppingMasterApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                Stock = command.Stock,
                CategoryId = command.CategoryId
            };

            return await _productRepository.AddAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id) ?? throw new ProductNotFoundException(id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }
    }
}
