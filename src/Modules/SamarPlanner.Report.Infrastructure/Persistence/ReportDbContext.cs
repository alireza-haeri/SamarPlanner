using Microsoft.EntityFrameworkCore;
using SamarPlanner.Report.Core.Entities;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Report.Infrastructure.Persistence;

public class ReportDbContext(DbContextOptions<ReportDbContext> options) : ModuleDbContext<ReportDbContext>(options)
{
    public override required string Schema { get; set; } = "Report";

    public DbSet<Core.Entities.Report> Reports { get; set; }
    public DbSet<ReportHighlight> Highlights { get; set; }
}