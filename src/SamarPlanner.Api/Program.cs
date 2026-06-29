using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Goal;
using SamarPlanner.Identity;
using SamarPlanner.Report;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
var applicationSettings = builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>()
    ?? throw new ArgumentNullException(nameof(ApplicationSettings));

builder.Services
    .AddIdentityServices()
    .AddGoalServices()
    .AddTaskServices()
    .AddReportServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebApplicationCors", policy =>
    {
        policy.WithOrigins(applicationSettings.CorsPolicy.Origins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.UseOneOfForPolymorphism();
    options.UseInlineDefinitionsForEnums();
    options.UseAllOfToExtendReferenceSchemas();
});

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapOpenApi();
app.MapSwagger();
app.MapScalarApiReference(options =>
{
    options.Title = "SamarPlanner API";
    options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("WebApplicationCors");

app.Run();