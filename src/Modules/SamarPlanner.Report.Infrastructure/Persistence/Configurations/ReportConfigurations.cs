using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SamarPlanner.Report.Infrastructure.Persistence.Configurations;

public class ReportConfigurations : IEntityTypeConfiguration<Core.Entities.Report>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Report> builder)
    {
        builder.ToTable(Core.Entities.Report.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x =>  x.UserId );

        builder.Property(x => x.Title)
            .IsRequired(false)
            .IsUnicode(true)
            .HasMaxLength(150);

        builder.Property(x => x.Note)
            .IsRequired(true)
            .IsUnicode(true)
            .HasMaxLength(1000);

        builder.Property(x => x.Score)
            .IsRequired(false);

        builder.Property(x => x.PeriodStart)
            .IsRequired(true);

        builder.Property(x => x.PeriodEnd)
            .IsRequired(true);

        builder.HasMany(x => x.Highlights)
            .WithOne(x => x.Report)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}