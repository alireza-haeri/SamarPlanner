using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Shared.Application.Extensions;

namespace SamarPlanner.Goal.Application;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
            {
                options.Lifetime = ServiceLifetime.Scoped;
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            })
            .AddGlobalBehaviors();

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return builder;
    }
}