using Microsoft.JSInterop;
using SamarPlanner.Web.Shared.Services;

namespace SamarPlanner.Maui;

public static class JsErrorLogger
{
    public static IServiceProvider? Services;

    [JSInvokable]
    public static async Task LogJsError(string message, string source, string line)
    {
        if (Services == null) return;

        var loggingService = Services.GetRequiredService<IClientLoggingService>();
        await loggingService.LogErrorAsync($"JS Error: {message} ({source}:{line})", source: "JavaScript");
    }
}