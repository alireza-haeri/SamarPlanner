using Microsoft.EntityFrameworkCore;

namespace SamarPlanner.Shared.Infrastructure;

public interface IModuleDbContext;

public abstract class ModuleDbContext<TDbContext>(DbContextOptions<TDbContext> options)
    : DbContext(options), IModuleDbContext
    where TDbContext : DbContext
{
    public abstract required string Schema { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}