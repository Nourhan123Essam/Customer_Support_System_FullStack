using CustomerSupport.Application.DTOs.Category;
using CustomerSupport.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupport.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

<<<<<<< HEAD
        [Authorize(Roles = "Admin,CustomerAgent")]
=======
        [Authorize(Roles = "Admin,SupportAgent")]
>>>>>>> bug-fix-branch
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var created = await _categoryService.CreateCategoryAsync(dto);
            return created ? Ok("Category created successfully") : BadRequest("Category already exists");
        }

<<<<<<< HEAD
        [Authorize(Roles = "Admin,CustomerAgent")]
=======
        [Authorize(Roles = "Admin,SupportAgent")]
>>>>>>> bug-fix-branch
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto)
        {
            var updated = await _categoryService.UpdateCategoryAsync(id, dto);
            return updated ? Ok("Category updated successfully") : NotFound("Category not found");
        }

<<<<<<< HEAD
        [Authorize(Roles = "Admin,CustomerAgent")]
=======
        [Authorize(Roles = "Admin,SupportAgent")]
>>>>>>> bug-fix-branch
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            return deleted ? Ok("Category deleted successfully") : NotFound("Category not found");
        }
    }
}
