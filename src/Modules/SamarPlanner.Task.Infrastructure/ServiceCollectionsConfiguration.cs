using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Infrastructure.Persistence;
using SamarPlanner.Task.Infrastructure.Repositories;

namespace SamarPlanner.Task.Infrastructure;

public static class ServiceCollectionsConfiguration
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;
        var databaseSettings = applicationSettings.Databases.TaskConnectionString;

        services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Task.Infrastructure.MigrationsHistoryTable");
            });
        });

        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}