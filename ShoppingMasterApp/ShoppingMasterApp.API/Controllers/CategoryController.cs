using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Exceptions;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return ApiSuccess(categories, "Categories retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return ApiNotFound($"Category with ID {id} not found");
            }

            return ApiSuccess(category, "Category retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            await _categoryService.CreateCategoryAsync(command);
            return ApiSuccess<object>(null, "Category created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            try
            {
                await _categoryService.UpdateCategoryAsync(command);
                return ApiSuccess<object>(null, "Category updated successfully");
            }
            catch (CategoryNotFoundException ex)
            {
                return ApiNotFound(ex.Message); // Kategori bulunamazsa anlamlı bir hata mesajı döner
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return ApiSuccess<object>(null, "Category deleted successfully");
            }
            catch (CategoryNotFoundException ex)
            {
                return ApiNotFound(ex.Message);
            }
        }
    }
}
