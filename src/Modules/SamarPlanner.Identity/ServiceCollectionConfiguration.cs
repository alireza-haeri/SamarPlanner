using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Identity.Core;
using SamarPlanner.Identity.Infrastructure;

namespace SamarPlanner.Identity;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services
            .ConfigureCore()
            .ConfigureInfrastructure();
        
        return services;
    }
}