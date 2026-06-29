using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamarPlanner.Report.Core.Entities;

namespace SamarPlanner.Report.Infrastructure.Persistence.Configurations;

public class ReportHighlightConfiguration : IEntityTypeConfiguration<ReportHighlight>
{
    public void Configure(EntityTypeBuilder<ReportHighlight> builder)
    {
        builder.ToTable(ReportHighlight.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Text);

        builder.Property(x => x.Text)
            .IsRequired()
            .IsUnicode(true)
            .HasMaxLength(500);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();
    }
}