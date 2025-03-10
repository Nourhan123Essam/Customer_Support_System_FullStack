using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerSupport.Infrastructure.Seeding
{
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Seeds roles (Admin, SupportAgent, Customer) if they do not exist.
        /// </summary>
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Customer", "SupportAgent", "Admin" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        /// <summary>
        /// Seeds an initial Admin user if no Admin exists.
        /// </summary>
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>(); // Retrieve config

            // Fetch admin credentials from configuration
            string adminEmail = configuration["AdminSettings:Email"] ?? "admin@support.com";
            string adminPassword = configuration["AdminSettings:Password"] ?? "Admin@123"; // Change in production!

            if (!await userManager.Users.AnyAsync(u => u.Email == adminEmail))
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }           
        }

        /// <summary>
        /// Runs all seeding methods.
        /// </summary>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            await SeedRolesAsync(serviceProvider);
            await SeedAdminUserAsync(serviceProvider);
        }

    }
}
