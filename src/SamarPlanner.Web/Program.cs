using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SamarPlanner.Web;
using SamarPlanner.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IApiClient,ApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5009/");
    client.DefaultRequestHeaders.Add("Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjkzMTdiZjlhLTU5MzMtNDNmNC03ZTA2LTA4ZGVjN2I1YWMzYiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL21vYmlsZXBob25lIjoiMDk5MDc5MTI4OTAiLCJleHAiOjE3ODI0NzIxNzcsImlzcyI6IlNhbWFyUGxhbm5lciIsImF1ZCI6IlNhbWFyUGxhbm5lciJ9.0iMnSOqBSqUtimlUrt9de0itsid9bN7eVoulBHkl3n4");
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

await builder.Build().RunAsync();