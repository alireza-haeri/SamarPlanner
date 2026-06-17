using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Task.Application;
using SamarPlanner.Task.Infrastructure;

namespace SamarPlanner.Task;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection AddTaskServices(this IServiceCollection services)
    {
        services.ConfigureApplication();
        services.ConfigureInfrastructure();
        return services;
    }
}