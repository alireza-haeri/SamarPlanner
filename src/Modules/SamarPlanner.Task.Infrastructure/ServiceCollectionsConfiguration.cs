using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Infrastructure.Persistence;
using SamarPlanner.Task.Infrastructure.Repositories;

namespace SamarPlanner.Task.Infrastructure;

public static class ServiceCollectionsConfiguration
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
            ?? throw new InvalidOperationException(nameof(ApplicationSettings));
        var databaseSettings = applicationSettings.Databases.TaskConnectionString;

        builder.Services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings,
                sqlOptions =>
                {
                    sqlOptions.MigrationsHistoryTable("SamarPlanner.Task.Infrastructure.MigrationsHistoryTable");
                });
        });

        builder.Services.AddScoped<ITaskRepository, TaskRepository>();

        return builder;
    }
}