using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> CreateCategory(CreateCategoryCommand command);
        Task<CategoryDto> UpdateCategory(UpdateCategoryCommand command);
        Task<bool> DeleteCategory(int id);
    }


}
