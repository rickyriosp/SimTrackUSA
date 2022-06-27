using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Ricky",
                Email = "ricky@mailinator.com",
                UserName = "ricky@mailinator.com",
                Address = new Address
                {
                    FirstName = "Ricky",
                    LastName = "Rios",
                    Street = "10 Main St.",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10462"
                }
            };

            await userManager.CreateAsync(user, "Password123!");
        }
    }
}
