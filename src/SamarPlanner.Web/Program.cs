using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SamarPlanner.Web;
using SamarPlanner.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5009/");
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
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ModalService>();

await builder.Build().RunAsync();