namespace SamarPlanner.Web.Shared.Abstractions;

public interface IStorageService
{
    Task SetValueAsync<T>(string key, T value);
    Task<T?> GetValueAsync<T>(string key);
    Task RemoveAsync(string key);
    Task ClearAsync();
}