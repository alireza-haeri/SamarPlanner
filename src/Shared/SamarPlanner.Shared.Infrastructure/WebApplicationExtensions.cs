using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Infrastructure;

public static class WebApplicationExtensions
{
    public static async System.Threading.Tasks.Task MigrateModuleDatabaseAsync<TDbContext>(this IHost app)
        where TDbContext : DbContext , IModuleDbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    
    public static WebApplicationBuilder AddSharedAuthentication(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>()
                          ?? throw new InvalidOperationException(nameof(ApplicationSettings));

        var jwtSettings = applicationSettings.JwtToken;

        var key = Encoding.UTF8.GetBytes(jwtSettings.SigningKey);

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}