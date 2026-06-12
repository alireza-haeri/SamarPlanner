using Microsoft.EntityFrameworkCore;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Goal.Infrastructure.Persistence;

public class GoalDbContext(DbContextOptions<GoalDbContext> options) : ModuleDbContext<GoalDbContext>(options)
{
    public override required string Schema { get; set; } = "Goal";

    public DbSet<Core.Entities.Goal> Goals { get; set; }
}