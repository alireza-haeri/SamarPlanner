using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamarPlanner.Identity.Core.Abstractions;
using SamarPlanner.Identity.Core.Entities;
using SamarPlanner.Identity.Infrastructure.Persistence;
using IdentityResult = SamarPlanner.Identity.Core.Contracts.IdentityResult;

namespace SamarPlanner.Identity.Infrastructure.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager, ILogger<UserRepository> logger) : IUserRepository
{
    public async Task<IdentityResult> CreateAsync(User user, string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var applicationUser = ApplicationUser.CrateFromUser(user);

            var result = await userManager.CreateAsync(applicationUser, password);

            return ToIdentityResult(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating user with phone number {PhoneNumber}", user.PhoneNumber);
            return new IdentityResult(false, new Dictionary<string, string[]>
                { { "General", ["خطا در هنگام ایجاد کاربر."] } }
            );
        }
    }

    public async Task<bool> CheckPasswordAsync(string phoneNumber, string password,
        CancellationToken cancellationToken = default)
    {
        var applicationUser =
            await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        if (applicationUser is null)
            return false;
        return await userManager.CheckPasswordAsync(applicationUser, password);
    }

    public async Task<User?> GetAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var applicationUser = await userManager.Users
            .SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        if (applicationUser is null)
            return null;

        return User.Create(applicationUser.Id, applicationUser.PhoneNumber!).Response;
    }

    private IdentityResult ToIdentityResult(Microsoft.AspNetCore.Identity.IdentityResult result) =>
        new
        (
            result.Succeeded,
            result.Errors
                .GroupBy(g => g.Code)
                .Select(g => new
                {
                    g.Key, IdentityErrors = g
                        .ToArray()
                        .Select(a => a.Description)
                        .ToArray()
                })
                .ToDictionary(k => k.Key, e => e.IdentityErrors)
        );
}