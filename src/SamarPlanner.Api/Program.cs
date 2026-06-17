using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Goal;
using SamarPlanner.Identity;
using SamarPlanner.Shared.Extensions;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services
    .AddIdentityServices()
    .AddGoalServices()
    .AddTaskServices();

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapOpenApi();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();