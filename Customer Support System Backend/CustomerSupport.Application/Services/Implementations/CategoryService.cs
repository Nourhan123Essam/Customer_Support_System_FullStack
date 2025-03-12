
using CustomerSupport.Application.DTOs.Category;
using CustomerSupport.Application.Services.Interfaces;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Interfaces;

namespace CustomerSupport.Application.Services.Implementations
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) { _categoryRepository = categoryRepository; }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto { CategoryId = c.CategoryId, Name = c.Name });
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : new CategoryDto { CategoryId = category.CategoryId, Name = category.Name };
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDto dto)
        {
            if (await _categoryRepository.ExistsAsync(dto.Name)) return false;
            await _categoryRepository.AddAsync(new Category { Name = dto.Name });
            return await _categoryRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;
            category.Name = dto.Name;
            await _categoryRepository.UpdateAsync(category);
            return await _categoryRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;
            await _categoryRepository.DeleteAsync(category);
            return await _categoryRepository.SaveChangesAsync() > 0;
        }
    }

}
