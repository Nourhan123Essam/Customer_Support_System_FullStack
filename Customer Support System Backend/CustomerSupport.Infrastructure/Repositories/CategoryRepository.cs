using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSupport.Domain.Entities;
using CustomerSupport.Domain.Interfaces;
using CustomerSupport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Infrastructure.Repositories
{
<<<<<<< HEAD
    public class CategoryRepository 
=======
    public class CategoryRepository: ICategoryRepository
>>>>>>> bug-fix-branch
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
        public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
        public async Task<bool> ExistsAsync(string name) => await _context.Categories.AnyAsync(c => c.Name == name);
        public async Task AddAsync(Category category) => await _context.Categories.AddAsync(category);
        public Task UpdateAsync(Category category) { _context.Categories.Update(category); return Task.CompletedTask; }
        public Task DeleteAsync(Category category) { _context.Categories.Remove(category); return Task.CompletedTask; }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
<<<<<<< HEAD

=======
>>>>>>> bug-fix-branch
}
