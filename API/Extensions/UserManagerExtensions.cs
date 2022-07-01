using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input,
        ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return await input.Users.Include(u => u.Address)
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public static async Task<AppUser> FindUserByClaimsPrincipleAsync(this UserManager<AppUser> input,
        ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return await input.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}
