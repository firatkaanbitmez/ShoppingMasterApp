using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.CQRS.Queries.Category;

namespace ShoppingMasterApp.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id) return ApiError("Invalid Category ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return ApiResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return ApiResponse(result);
        }
    }
}
