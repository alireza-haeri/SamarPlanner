using Microsoft.EntityFrameworkCore;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Infrastructure;

namespace SamarPlanner.Note.Infrastructure.Persistence;

public class NoteDbContext(DbContextOptions<NoteDbContext> options) : ModuleDbContext<NoteDbContext>(options)
{
    public DbSet<Core.Entities.Note> Notes { get; set; }
    public DbSet<NoteFile> NoteFiles { get; set; }
    public DbSet<NoteCategory> NoteCategories { get; set; }
    
    public override required string Schema { get; set; } = "note";
}