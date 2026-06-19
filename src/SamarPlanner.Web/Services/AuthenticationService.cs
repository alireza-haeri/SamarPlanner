using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace SamarPlanner.Web.Services;

public interface IAuthenticationService
{
    Task LoginAsync(string token, int expiresInMinutes);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
    Task<bool> IsAuthenticatedAsync();
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IStorageService _storage;
    private readonly NavigationManager _navigationManager;

    private const string TokenKey = "authToken";
    private const string ExpirationKey = "authExpiration";

    public AuthenticationService(IStorageService storage, NavigationManager navigationManager)
    {
        _storage = storage;
        _navigationManager = navigationManager;
    }

    public async Task LoginAsync(string token, int expiresInMinutes)
    {
        var expiration = DateTime.UtcNow.AddMinutes(expiresInMinutes);
        await _storage.SetValueAsync(TokenKey, token);
        await _storage.SetValueAsync(ExpirationKey, expiration);
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveAsync(TokenKey);
        await _storage.RemoveAsync(ExpirationKey);
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _storage.GetValueAsync<string>(TokenKey);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            return false;

        try
        {
            var expiration = await _storage.GetValueAsync<DateTime?>(ExpirationKey);
            if (expiration.HasValue && expiration.Value < DateTime.UtcNow)
            {
                await LogoutAsync();
                return false;
            }
        }
        catch
        {
            return false;
        }

        return true;
    }
}