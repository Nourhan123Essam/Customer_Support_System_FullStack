<<<<<<< HEAD
﻿using CustomerSupport.Domain.Entities;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSupport.Domain.Entities;
>>>>>>> bug-fix-branch
using CustomerSupport.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerSupport.Infrastructure.Seeding
{
    public static class CategorySeeder
    {
        public static async Task SeedCategoriesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var requiredCategories = new List<string> { "Bug", "Feature Request", "Feedback" };

            foreach (var categoryName in requiredCategories)
            {
                var exists = await dbContext.Categories.AnyAsync(c => c.Name == categoryName);
                if (!exists)
                {
                    await dbContext.Categories.AddAsync(new Category { Name = categoryName });
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
<<<<<<< HEAD

=======
>>>>>>> bug-fix-branch
}
