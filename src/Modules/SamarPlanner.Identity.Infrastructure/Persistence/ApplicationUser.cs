using Microsoft.AspNetCore.Identity;
using SamarPlanner.Identity.Core.Entities;

namespace SamarPlanner.Identity.Infrastructure.Persistence;

public class ApplicationUser : IdentityUser<Guid>
{
    public static ApplicationUser CrateFromUser(User user) =>
        new()
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            UserName = user.PhoneNumber
        };
}