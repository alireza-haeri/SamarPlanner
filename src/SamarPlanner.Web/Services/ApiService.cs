using System.Net;
using System.Text.Json;

namespace SamarPlanner.Web.Services;

public interface IApiService
{
    Task<TResult?> SendAsync<TResult>(Func<Task<TResult>> apiCall, string? successMessage = null,
        bool showErrorToast = true);

    Task<bool> SendAsync(Func<Task> apiCall, string? successMessage = null, bool showErrorToast = true);
}

public class ApiService(IToastService toastService) : IApiService
{
    public async Task<TResult?> SendAsync<TResult>(Func<Task<TResult>> apiCall, string? successMessage = null,
        bool showErrorToast = true)
    {
        try
        {
            var result = await apiCall();

            if (!string.IsNullOrEmpty(successMessage))
                toastService.ShowToastSuccess(successMessage);

            return result;
        }
        catch (ApiException ex)
        {
            if (showErrorToast)
            {
                var message = GetErrorMessage(ex.StatusCode, ex.Response);
                toastService.ShowToastError( message);
            }

            return default;
        }
        catch (HttpRequestException ex)
        {
            if (showErrorToast)
                toastService.ShowToastError( "خطای شبکه! لطفاً اتصال اینترنت خود را بررسی کنید.");

            return default;
        }
        catch (JsonException)
        {
            if (showErrorToast)
                toastService.ShowToastError( "خطا در پردازش پاسخ سرور!");

            return default;
        }
        catch (Exception ex)
        {
            if (showErrorToast)
                toastService.ShowToastError( $"خطای غیرمنتظره: {ex.Message}");

            return default;
        }
    }
    public async Task<bool> SendAsync(Func<Task> apiCall, string? successMessage = null, bool showErrorToast = true)
    {
        try
        {
            await apiCall();

            if (!string.IsNullOrEmpty(successMessage))
                toastService.ShowToastSuccess(successMessage);

            return true;
        }
        catch (ApiException ex)
        {
            if (showErrorToast)
            {
                var message = GetErrorMessage(ex.StatusCode, ex.Response);
                toastService.ShowToastError(message);
            }

            return false;
        }
        catch (HttpRequestException)
        {
            if (showErrorToast)
                toastService.ShowToastError("خطای شبکه! لطفاً اتصال اینترنت خود را بررسی کنید.");
            return false;
        }
        catch (Exception ex)
        {
            if (showErrorToast)
                toastService.ShowToastError( $"خطای غیرمنتظره: {ex.Message}");
            return false;
        }
    }

    private string GetErrorMessage(int statusCode, string? response)
    {
        try
        {
            if (!string.IsNullOrEmpty(response))
            {
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (root.TryGetProperty("errors", out var errorsElement))
                {
                    var errorMessages = new List<string>();
                    foreach (var property in errorsElement.EnumerateObject())
                    {
                        var fieldErrors = property.Value.EnumerateArray().Select(e => e.GetString());
                        errorMessages.AddRange(fieldErrors!);
                    }

                    return string.Join("\n", errorMessages);
                }

                if (root.TryGetProperty("title", out var titleElement))
                    return titleElement.GetString() ?? $"خطا ({statusCode})";

                if (root.TryGetProperty("message", out var messageElement))
                    return messageElement.GetString() ?? $"خطا ({statusCode})";
            }
        }
        catch
        {
            // ignored
        }

        return statusCode switch
        {
            400 => "درخواست نامعتبر! لطفاً اطلاعات را بررسی کنید.",
            401 => "شما اجازه دسترسی ندارید. لطفاً وارد شوید.",
            403 => "شما مجوز دسترسی به این بخش را ندارید.",
            404 => "منبع درخواستی یافت نشد!",
            409 => "اطلاعات تکراری! لطفاً دوباره تلاش کنید.",
            500 => "خطای سرور! لطفاً بعداً تلاش کنید.",
            _ => $"خطا ({statusCode}). لطفاً دوباره تلاش کنید."
        };
    }
}