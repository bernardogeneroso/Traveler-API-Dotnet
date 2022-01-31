using Microsoft.AspNetCore.Identity;
using Models;

namespace Database;

public class Seed
{
    public static async Task SeedData(UserManager<AppUser> userManager, DataContext context)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser
                {
                    DisplayName = "Bob Beta",
                    UserName = "bobeta",
                    Email = "bob@test.com",
                },
                new AppUser
                {
                    DisplayName = "Jimy Tester",
                    UserName = "jimytester",
                    Email = "jimy@test.com",
                },
                new AppUser
                {
                    DisplayName = "Admin Master",
                    UserName = "adminmaster",
                    Email = "admin@test.com",
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
