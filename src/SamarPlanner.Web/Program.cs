using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SamarPlanner.Web;
using SamarPlanner.Web.Services;
using SamarPlanner.Web.Shared;
using SamarPlanner.Web.Shared.Abstractions;
using SamarPlanner.Web.Shared.Configuration;
using SamarPlanner.Web.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection(ApiSettings.SectionName));
var apiSettings = builder.Configuration
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

builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ModalService>();
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
await host.RunAsync();