using SamarPlanner.Web.Shared.Abstractions;

namespace SamarPlanner.Maui.Services;

public class MauiAuthenticationService(IStorageService storageService) : IAuthenticationService
{
    private const string TokenKey = "authToken";

    public async Task LoginAsync(string token, int expiresInMinutes)
    {
        await storageService.SetValueAsync(TokenKey, token);
        // اگه نیاز به تاریخ انقضا داری، می‌تونی ذخیره کنی
        await storageService.SetValueAsync("auth_token_expiry", DateTime.UtcNow.AddMinutes(expiresInMinutes));
    }

    public async Task LogoutAsync()
    {
        await storageService.RemoveAsync(TokenKey);
        await storageService.RemoveAsync("auth_token_expiry");
    }

    public async Task<string?> GetTokenAsync()
    {
        return await storageService.GetValueAsync<string>(TokenKey);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrEmpty(token))
            return false;

        // چک کردن انقضا (اختیاری)
        var expiry = await storageService.GetValueAsync<DateTime?>("auth_token_expiry");
        if (expiry.HasValue && expiry.Value < DateTime.UtcNow)
            return false;

        return true;
    }
}