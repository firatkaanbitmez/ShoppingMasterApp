using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.CQRS.Queries.Product;

namespace ShoppingMasterApp.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id) return ApiError("Invalid Product ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });
            return ApiResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return ApiResponse(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var result = await _mediator.Send(new GetProductsByCategoryQuery { CategoryId = categoryId });
            return ApiResponse(result);
        }

        [HttpPut("{id}/stock")]
        public async Task<IActionResult> ChangeProductStock(int id, ChangeProductStockCommand command)
        {
            if (id != command.ProductId) return ApiError("Invalid Product ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }
    }
}
