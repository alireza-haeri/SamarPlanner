using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Goal.Application;
using SamarPlanner.Goal.Infrastructure;
using SamarPlanner.Goal.Infrastructure.Persistence;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Goal;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder AddGoalServices(this WebApplicationBuilder builder)
    {
        builder
            .ConfigureApplication()
            .ConfigureInfrastructure();
        
        return builder;
    }
    
    public static async Task<WebApplication> UseGoalModuleAsync(this WebApplication app)
    {
        await app.MigrateModuleDatabaseAsync<GoalDbContext>();
        return app;
    }
}