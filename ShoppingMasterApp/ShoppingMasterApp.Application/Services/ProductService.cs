using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Exceptions;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.GetAllAsync();
        var filteredProducts = products.Where(p => p.CategoryId == categoryId);
        return _mapper.Map<IEnumerable<ProductDto>>(filteredProducts);
    }
    public async Task ChangeProductStockAsync(ChangeProductStockCommand command)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId);
        if (product != null)
        {
            product.Stock = command.Stock;
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<ProductDto>> GetPagedProductsAsync(PagedQuery query)
    {
        var products = await _productRepository.GetPagedProductsAsync(query);
        return products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price.Amount,  // Assuming Price is a value object
            Stock = product.Stock,
            CategoryName = product.Category.Name
        }).ToList();
    }

    public async Task CreateProductAsync(CreateProductCommand command)
    {
        var product = _mapper.Map<Product>(command);
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(UpdateProductCommand command)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        if (product == null) throw new ProductNotFoundException(command.Id);

        _mapper.Map(command, product);
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) throw new ProductNotFoundException(id);

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
    }
}
