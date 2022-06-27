using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
    {
        try
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
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<AppIdentityDbContextSeed>();
            logger.LogError(ex, "An error occurred during identity data seed");
        }
    }
}
