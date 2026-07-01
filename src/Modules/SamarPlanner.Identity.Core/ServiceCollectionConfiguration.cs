using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Identity.Core.UseCases.Commands.RegisterOrLogin;
using SamarPlanner.Shared.Application.Behaviors;
using SamarPlanner.Shared.Application.Extensions;
using SamarPlanner.Shared.Contracts;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Identity.Core;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder ConfigureCore(this WebApplicationBuilder builder)
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