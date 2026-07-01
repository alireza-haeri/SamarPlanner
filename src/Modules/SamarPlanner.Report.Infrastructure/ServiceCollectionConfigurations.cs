using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Report.Infrastructure.Persistence;
using SamarPlanner.Report.Infrastructure.Repositories;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Infrastructure;

public static class ServiceCollectionConfigurations
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
            ?? throw new InvalidOperationException(nameof(ApplicationSettings));
        var databaseSettings = applicationSettings.Databases.ReportConnectionString;

        builder.Services.AddDbContext<ReportDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Report.Infrastructure.MigrationsHistoryTable");
            });
        });

        builder.Services.AddScoped<IReportRepository, ReportRepository>();
        
        return builder;
    }
}