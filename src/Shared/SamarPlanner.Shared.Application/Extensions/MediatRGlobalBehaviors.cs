using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Shared.Application.Behaviors;

namespace SamarPlanner.Shared.Application.Extensions;

public static class MediatRGlobalBehaviors
{
    public static MediatRServiceConfiguration AddGlobalBehaviors(this MediatRServiceConfiguration configs)
    {
        configs.AddOpenBehavior(typeof(ValidationBehavior<,>));
        
        return configs;
    }
}