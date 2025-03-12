using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSupport.Application.DTOs.Category;

namespace CustomerSupport.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<bool> CreateCategoryAsync(CreateCategoryDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
