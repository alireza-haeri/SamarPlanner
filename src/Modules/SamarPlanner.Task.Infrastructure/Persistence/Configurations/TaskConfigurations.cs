using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamarPlanner.Task.Core.Entities;

namespace SamarPlanner.Task.Infrastructure.Persistence.Configurations;

public class TaskConfigurations : IEntityTypeConfiguration<Core.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Task> builder)
    {
        builder.ToTable(Core.Entities.Task.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => new { x.Id, x.UserId });

        builder.Property(x => x.Title)
            .IsRequired()
            .IsUnicode(true)
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .IsUnicode(true)
            .HasMaxLength(1000);

        builder.Property(x => x.Priority)
            .HasConversion<string>()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(x => x.DefaultTime)
            .IsUnicode(false);

        builder.Property(x => x.SoftDeleted)
            .IsUnicode(false);

        builder.OwnsOne<RepeatPattern>(r => r.RepeatPattern, rBuilder =>
        {
            rBuilder.ToJson();

            rBuilder.Property(p => p.Type)
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(50);

            rBuilder.Property(x => x.AnchorDate)
                .IsUnicode(false);

            rBuilder.Property(x => x.Interval)
                .IsUnicode(false)
                .HasMaxLength(50);

            rBuilder.Property(x => x.WeekDays)
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(50);

            rBuilder.Property(x => x.MonthDays)
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(50);
        });

        builder.HasMany(t => t.Occurrences)
            .WithOne(o=>o.Task)
            .HasForeignKey(t => t.TaskId);
        
        builder.HasQueryFilter(t => !t.SoftDeleted);
    }
}