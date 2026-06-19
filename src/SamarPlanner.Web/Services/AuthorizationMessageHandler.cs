// Services/AuthorizationMessageHandler.cs
using System.Net.Http.Headers;

namespace SamarPlanner.Web.Services;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly IAuthenticationService _authService;

    public AuthorizationMessageHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _authService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}