using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Exceptions;
using ShoppingMasterApp.Domain.Models;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return ApiSuccess(products, "Products retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return ApiSuccess(product, "Product retrieved successfully");
            }
            catch (ProductNotFoundException ex)
            {
                return ApiNotFound(ex.Message); // Ürün bulunamadığında anlamlı hata döner
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _productService.CreateProductAsync(command);
            return ApiSuccess<object>(null, "Product created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            try
            {
                command.Id = id;
                await _productService.UpdateProductAsync(command);
                return ApiSuccess<object>(null, "Product updated successfully");
            }
            catch (ProductNotFoundException ex)
            {
                return ApiNotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return ApiSuccess<object>(null, "Product deleted successfully");
            }
            catch (ProductNotFoundException ex)
            {
                return ApiNotFound(ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts([FromQuery] PagedQuery query)
        {
            var products = await _productService.GetPagedProductsAsync(query);
            return ApiSuccess(products, "Paged products retrieved successfully");
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return ApiSuccess(products, "Products retrieved successfully by category");
        }
    }
}
