using BookNest.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookNest.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (! await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (! await roleManager.RoleExistsAsync(Roles.Member))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Member));
            }

            if (! await roleManager.RoleExistsAsync(Roles.Librarian))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Librarian));
            }
        }
    }
}
