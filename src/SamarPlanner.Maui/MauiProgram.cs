using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Blazored.Toast;
using SamarPlanner.Web.Shared.Abstractions;
using SamarPlanner.Web.Shared.Services;
using SamarPlanner.Maui.Services;
using SamarPlanner.Web.Services;

namespace SamarPlanner.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        // builder.Services.AddBlazorWebViewDeveloperTools();
        // builder.Services.AddHybridWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
        

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            var ex = args.ExceptionObject as Exception;
            System.Diagnostics.Debug.WriteLine($"[UNHANDLED] {ex}");
            Android.Util.Log.Error("SamarPlanner", $"UNHANDLED EXCEPTION: {ex}");
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            System.Diagnostics.Debug.WriteLine($"[UNOBSERVED TASK] {args.Exception}");
            Android.Util.Log.Error("SamarPlanner", $"UNOBSERVED TASK EXCEPTION: {args.Exception}");
            args.SetObserved();
        };

        // ===== Authentication / Authorization (همون چیزی که خطا میداد) =====
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());

        // ===== سرویس‌های واقعی برای MAUI =====
        builder.Services.AddScoped<IStorageService, MauiStorageService>();
        builder.Services.AddScoped<IAuthenticationService, MauiAuthenticationService>();

        builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.1.10:5009/");
            }).AddTypedClient<IApiClient>((httpClient, sp) =>
            {
                var apiClient = new ApiClient(httpClient)
                {
                    ReadResponseAsString = true
                };
                return apiClient;
            })
            .AddHttpMessageHandler<AuthorizationMessageHandler>();

        // اگه IApiClient هم از NSwag تولید شده و در Web.Shared هست، همینجا هم رجیسترش کن:
        // builder.Services.AddScoped<IApiClient>(sp =>
        //     new ApiClient(sp.GetRequiredService<HttpClient>()) { ReadResponseAsString = true });

        // ===== سرویس‌های دیگه از RCL =====
        builder.Services.AddBlazoredToast();
        builder.Services.AddScoped<IToastService, ToastService>();
        builder.Services.AddScoped<IApiService, ApiService>();
        builder.Services.AddScoped<ModalService>();
        builder.Services.AddScoped<AuthorizationMessageHandler>();
        builder.Services.AddSingleton<BackNavigationService>();

        return builder.Build();
    }
}