using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SamarPlanner.Goal.Core.Entities;

namespace SamarPlanner.Goal.Infrastructure.Persistence.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Core.Entities.Goal>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Goal> builder)
    {
        builder.ToTable(Core.Entities.Goal.TableName);

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Id, x.UserId });
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.GoalType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.GoalPriority)
            .IsRequired(false)
            .HasConversion<string>();
    }
}