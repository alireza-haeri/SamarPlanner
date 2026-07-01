using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Identity.Core;
using SamarPlanner.Identity.Infrastructure;
using SamarPlanner.Identity.Infrastructure.Persistence;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Identity;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder
            .ConfigureCore()
            .ConfigureInfrastructure();
        
        return builder;
    }
    
    public static async Task<WebApplication> UseIdentityModuleAsync(this WebApplication app)
    {
        await app.MigrateModuleDatabaseAsync<IdentityDbContext>();
        return app;
    }
}