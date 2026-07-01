using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SamarPlanner.Identity.Core.Abstractions;
using SamarPlanner.Identity.Infrastructure.Persistence;
using SamarPlanner.Identity.Infrastructure.Repositories;
using SamarPlanner.Identity.Infrastructure.Services;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Identity.Infrastructure;

public static class ServiceCollectionConfiguration
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationSettings =
            builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
            ?? throw new InvalidOperationException(nameof(ApplicationSettings));
        var databaseSettings = applicationSettings.Databases;
        var jwtSettings = applicationSettings.JwtToken;

        builder.Services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.IdentityConnectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsHistoryTable(
                        "SamarPlanner.Identity.Infrastructure.MigrationsHistoryTable");
                });
        });

        // Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}