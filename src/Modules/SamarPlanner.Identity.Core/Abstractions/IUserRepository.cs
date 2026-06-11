using SamarPlanner.Identity.Core.Contracts;
using SamarPlanner.Identity.Core.Entities;

namespace SamarPlanner.Identity.Core.Abstractions;

public interface IUserRepository
{
    Task<IdentityResult> CreateAsync(User user,string password,CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(string phoneNumber, string password, CancellationToken cancellationToken = default);
    Task<User?> GetAsync(string phoneNumber, CancellationToken cancellationToken = default);
}