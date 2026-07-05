using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamarPlanner.Note.Core.Entities;

namespace SamarPlanner.Note.Infrastructure.Persistence.Configurations;

public class NoteCategoryConfiguration : IEntityTypeConfiguration<NoteCategory>
{
    public void Configure(EntityTypeBuilder<NoteCategory> builder)
    {
        builder.ToTable(NoteCategory.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .IsUnicode(true)
            .HasMaxLength(200);

        builder.HasMany(x => x.Notes)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}