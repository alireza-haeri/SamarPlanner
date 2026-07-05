using Microsoft.OpenApi;
using SamarPlanner.Goal;
using SamarPlanner.Identity;
using SamarPlanner.Note;
using SamarPlanner.Report;
using SamarPlanner.Shared.Infrastructure;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Shared.Swagger;
using SamarPlanner.Task;
using Scalar.AspNetCore;
using Serilog;

const string webApplicationCorsPolicyName = "WebApplicationCors";
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine(builder.Environment.ContentRootPath, "Logs", "log-.txt"),
        rollingInterval: RollingInterval.Day
    )
);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
var applicationSettings = builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>()
                          ?? throw new InvalidOperationException(nameof(ApplicationSettings));

builder
    .AddTaskServices()
    .AddReportServices()
    .AddIdentityServices()
    .AddGoalServices()
    .AddNoteServices();

builder.Services.AddControllers();
builder.AddSharedAuthentication();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
    options.AddPolicy(webApplicationCorsPolicyName, policy =>
        policy.WithOrigins(applicationSettings.CorsPolicy.Origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SamarPlanner API", Version = "v1" });

    options.EnableAnnotations();
    options.UseOneOfForPolymorphism();
    options.UseInlineDefinitionsForEnums();
    options.UseAllOfToExtendReferenceSchemas();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "توکن JWT را وارد کنید (نیازی به نوشتن کلمه‌ی Bearer نیست)"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });

    options.OperationFilter<SummaryFromOperationIdFilter>();
});

var app = builder.Build();

app.MapSwagger();
app.MapScalarApiReference(options =>
{
    options.Title = "SamarPlanner API";
    options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");

    options.Authentication = new ScalarAuthenticationOptions
    {
        PreferredSecuritySchemes = ["Bearer"]
    };

    options.OperationTitleSource = OperationTitleSource.Summary;
    options.Layout = ScalarLayout.Classic;
    options.Theme = ScalarTheme.BluePlanet;
    options.HiddenClients = true;
});

app.UseRouting();

app.UseCors(webApplicationCorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    await app.UseTaskModuleAsync();
    await app.UseReportModuleAsync();
    await app.UseIdentityModuleAsync();
    await app.UseGoalModuleAsync();
    await app.UseNoteModuleAsync();

    Log.Information("Starting SamarPlanner API");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}