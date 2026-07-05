using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Infrastructure.Persistence;
using SamarPlanner.Note.Infrastructure.Repositories;
using SamarPlanner.Note.Infrastructure.Services;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Infrastructure;

public static class ServiceCollectionConfigurations
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
                                  ?? throw new InvalidOperationException(nameof(ApplicationSettings));
        var databaseSettings = applicationSettings.Databases;
        
        builder.Services.AddDbContext<NoteDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.NoteConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Note.Infrastructure.MigrationsHistoryTable");
            });
        });

        builder.Services.AddScoped<INoteRepository, NoteRepository>();
        builder.Services.AddScoped<INoteFileRepository, NoteFileRepository>();
        builder.Services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();

        builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
        
        return builder;
    }
}