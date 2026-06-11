using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;
        var databaseSettings = applicationSettings.Databases;
        var jwtSettings = applicationSettings.JwtToken;

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.IdentityConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable("SamarPlanner.Identity.Infrastructure.MigrationsHistoryTable");
            });
        });

        // Identity
        services.AddIdentity<ApplicationUser,IdentityRole<Guid>>(options =>
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
        
        // JWT Authentication
        var key = Encoding.UTF8.GetBytes(jwtSettings.SigningKey);
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidAudience =  jwtSettings.Audience,
                    ValidateAudience = true,
                    ValidIssuer =  jwtSettings.Issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}