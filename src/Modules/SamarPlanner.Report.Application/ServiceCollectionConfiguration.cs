using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Shared.Application.Extensions;

namespace SamarPlanner.Report.Application;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            options.AddGlobalBehaviors();
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}