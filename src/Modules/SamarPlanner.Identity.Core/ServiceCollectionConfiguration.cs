using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Identity.Core.UseCases.Commands.RegisterOrLogin;
using SamarPlanner.Shared.Contracts;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Identity.Core;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection ConfigureCore(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}