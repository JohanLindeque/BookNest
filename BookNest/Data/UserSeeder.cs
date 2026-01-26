using BookNest.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookNest.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userManager, "admin@booknest.com", "P@ssw0rd", Roles.Admin);
            await CreateUserWithRole(userManager, "member@booknest.com", "P@ssw0rd", Roles.Member);
            await CreateUserWithRole(userManager, "librarian@booknest.com", "P@ssw0rd", Roles.Librarian);

        }

        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
