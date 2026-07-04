using SamarPlanner.Web.Shared.Abstractions;
using Microsoft.Maui.Storage;

namespace SamarPlanner.Maui.Services;

public class MauiStorageService : IStorageService
{
    public Task SetValueAsync<T>(string key, T value)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(value);
        Preferences.Default.Set(key, json);
        return Task.CompletedTask;
    }

    public Task<T?> GetValueAsync<T>(string key)
    {
        var json = Preferences.Default.Get<string>(key, null);
        if (string.IsNullOrEmpty(json))
            return Task.FromResult<T?>(default);

        var result = System.Text.Json.JsonSerializer.Deserialize<T>(json);
        return Task.FromResult(result);
    }

    public Task RemoveAsync(string key)
    {
        Preferences.Default.Remove(key);
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        Preferences.Default.Clear();
        return Task.CompletedTask;
    }
}