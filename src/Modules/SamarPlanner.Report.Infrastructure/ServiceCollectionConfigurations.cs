using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SamarPlanner.Report.Infrastructure.Persistence;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Infrastructure;

public static class ServiceCollectionConfigurations
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;
        var databaseSettings = applicationSettings.Databases.ReportConnectionString;

        services.AddDbContext<ReportDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Report.Infrastructure.MigrationsHistoryTable");
            });
        });

        return services;
    }
}