using Microsoft.EntityFrameworkCore;
using SamarPlanner.Shared.Infrastructure;
using SamarPlanner.Task.Core.Entities;

namespace SamarPlanner.Task.Infrastructure.Persistence;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : ModuleDbContext<TaskDbContext>(options)
{
    public override required string Schema { get; set; } = "task";

    public DbSet<Core.Entities.Task> Tasks { get; set; }
    public DbSet<TaskOccurrence> TaskOccurrences { get; set; }
}