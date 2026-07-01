using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SamarPlanner.Shared.Infrastructure;
using SamarPlanner.Task.Application;
using SamarPlanner.Task.Infrastructure;
using SamarPlanner.Task.Infrastructure.Persistence;

namespace SamarPlanner.Task;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder AddTaskServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureApplication();
        builder.ConfigureInfrastructure();
        return builder;
    }

    public static async Task<WebApplication> UseTaskModuleAsync(this WebApplication app)
    {
        await app.MigrateModuleDatabaseAsync<TaskDbContext>();
        return app;
    }
}