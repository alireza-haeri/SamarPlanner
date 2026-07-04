using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Blazored.Toast;
using Microsoft.Extensions.Configuration;
using SamarPlanner.Web.Shared.Abstractions;
using SamarPlanner.Web.Shared.Services;
using SamarPlanner.Maui.Services;
using SamarPlanner.Web.Shared.Configuration;

namespace SamarPlanner.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        // builder.Services.AddBlazorWebViewDeveloperTools();
        // builder.Services.AddHybridWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

        // ===== Authentication / Authorization (همون چیزی که خطا میداد) =====
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());

        // ===== سرویس‌های واقعی برای MAUI =====
        builder.Services.AddScoped<IStorageService, MauiStorageService>();
        builder.Services.AddScoped<IAuthenticationService, MauiAuthenticationService>();

        // خوندن appsettings.json از Raw Assets
        using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json")
            .GetAwaiter().GetResult();

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        builder.Services.Configure<ApiSettings>(
            config.GetSection(ApiSettings.SectionName));

        var apiSettings = config
                              .GetSection(ApiSettings.SectionName)
                              .Get<ApiSettings>()
                          ?? throw new InvalidOperationException("ApiSettings not configured");


        builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
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

        builder.Services.AddScoped<IClientLoggingService, ClientLoggingService>();

        var host = builder.Build();

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var loggingService = host.Services.GetRequiredService<IClientLoggingService>();
            var exception = args.ExceptionObject as Exception;
            loggingService.LogErrorAsync("Unhandled AppDomain Exception", exception, "AppDomain");
        };

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            var loggingService = host.Services.GetRequiredService<IClientLoggingService>();
            loggingService.LogErrorAsync("Unobserved Task Exception", args.Exception, "TaskScheduler");
            args.SetObserved();
        };

        JsErrorLogger.Services = host.Services;
        return host;
    }
}