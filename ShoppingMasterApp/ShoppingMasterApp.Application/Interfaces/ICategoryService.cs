using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryCommand command);
        Task UpdateCategoryAsync(UpdateCategoryCommand command);
        Task DeleteCategoryAsync(DeleteCategoryCommand command);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    }

}
