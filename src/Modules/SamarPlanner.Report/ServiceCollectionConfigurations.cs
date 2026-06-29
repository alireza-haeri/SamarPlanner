using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Report.Application;
using SamarPlanner.Report.Infrastructure;

namespace SamarPlanner.Report;

public static class ServiceCollectionConfigurations
{
    public static IServiceCollection AddReportServices(this IServiceCollection services)
    {
        services.ConfigureApplication();
        services.ConfigureInfrastructure();
        
        return services;
    }
}