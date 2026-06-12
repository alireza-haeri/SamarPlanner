using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Goal.Application;
using SamarPlanner.Goal.Infrastructure;

namespace SamarPlanner.Goal;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection AddGoalServices(this IServiceCollection services)
    {
        services
            .ConfigureApplication()
            .ConfigureInfrastructure();
        
        return services;
    }
}