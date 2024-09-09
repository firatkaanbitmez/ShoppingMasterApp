using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.CQRS.Queries.Product;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _productService.CreateProductAsync(command);
            return ApiResponse("Product created successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _productService.UpdateProductAsync(command);
            return ApiResponse("Product updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(new DeleteProductCommand { Id = id });
            return ApiResponse("Product deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();  // Tüm ürünleri getirir
            return ApiResponse(result);
        }


    }
}
