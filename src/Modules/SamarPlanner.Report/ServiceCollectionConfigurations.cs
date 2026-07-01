using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Report.Application;
using SamarPlanner.Report.Infrastructure;
using SamarPlanner.Report.Infrastructure.Persistence;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Report;

public static class ServiceCollectionConfigurations
{
    public static WebApplicationBuilder AddReportServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureApplication();
        builder.ConfigureInfrastructure();
        
        return builder;
    }
    
    public static async Task<WebApplication> UseReportModuleAsync(this WebApplication app)
    {
         await app.MigrateModuleDatabaseAsync<ReportDbContext>();
         return app;
    }
}