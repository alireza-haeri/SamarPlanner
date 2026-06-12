using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Goal;
using SamarPlanner.Identity;
using SamarPlanner.Shared.Extensions;
using SamarPlanner.Shared.Kernel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services
    .AddIdentityServices()
    .AddGoalServices();
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();