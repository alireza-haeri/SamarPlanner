using System.Security.Claims;

namespace SamarPlanner.Web.Shared.Abstractions;

public interface IAuthenticationService
{
    Task LoginAsync(string token, int expiresInMinutes);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
    Task<bool> IsAuthenticatedAsync();
}
