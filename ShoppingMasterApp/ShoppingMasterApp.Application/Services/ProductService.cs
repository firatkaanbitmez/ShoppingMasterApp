using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
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

    public async Task CreateProductAsync(CreateProductCommand command)
    {
        var product = _mapper.Map<Product>(command);
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(UpdateProductCommand command)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        if (product != null)
        {
            _mapper.Map(command, product);
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(DeleteProductCommand command)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        if (product != null)
        {
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
