using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamarPlanner.Task.Core.Entities;

namespace SamarPlanner.Task.Infrastructure.Persistence.Configurations;

public class TaskOccurrenceConfiguration : IEntityTypeConfiguration<TaskOccurrence>
{
    public void Configure(EntityTypeBuilder<TaskOccurrence> builder)
    {
        builder.ToTable(TaskOccurrence.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Date)
            .IsUnicode(false);
        
        builder.Property(x => x.Time)
            .IsUnicode(false);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsUnicode(false)
            .HasMaxLength(50);
        
        builder.Property(x => x.Score)
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.IsSkipped)
            .IsUnicode(false);
    }
}