using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Goal.Infrastructure.Persistence;
using SamarPlanner.Goal.Infrastructure.Repositories;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Infrastructure;

public static class ServiceCollectionConfiguration
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;
        var databaseSettings = applicationSettings.Databases;
        
        services.AddDbContext<GoalDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.GoalConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Goal.Infrastructure.MigrationsHistoryTable");
            });
        });

        services.AddScoped<IGoalRepository, GoalRepository>();
        
        return services;
    }
}