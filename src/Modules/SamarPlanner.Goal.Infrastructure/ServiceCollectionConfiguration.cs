using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Goal.Infrastructure.Persistence;
using SamarPlanner.Goal.Infrastructure.Repositories;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Infrastructure;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder) 
    {
        var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
            ?? throw new InvalidOperationException(nameof(ApplicationSettings));
        var databaseSettings = applicationSettings.Databases;
        
        builder.Services.AddDbContext<GoalDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.GoalConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Goal.Infrastructure.MigrationsHistoryTable");
            });
        });

        builder.Services.AddScoped<IGoalRepository, GoalRepository>();
        
        return builder;
    }
}