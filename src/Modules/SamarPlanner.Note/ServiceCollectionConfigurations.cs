using Microsoft.AspNetCore.Builder;
using SamarPlanner.Note.Application;
using SamarPlanner.Note.Infrastructure;
using SamarPlanner.Note.Infrastructure.Persistence;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Note;

public static class ServiceCollectionConfigurations
{
    public static WebApplicationBuilder AddNoteServices(this WebApplicationBuilder builder)
    {
        builder
            .ConfigureApplication()
            .ConfigureInfrastructure();
        
        return builder;
    }
    
    public static async Task<WebApplication> UseNoteModuleAsync(this WebApplication app)
    {
        await app.MigrateModuleDatabaseAsync<NoteDbContext>();
        return app;
    }
}