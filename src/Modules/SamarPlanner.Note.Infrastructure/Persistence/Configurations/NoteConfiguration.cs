using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SamarPlanner.Note.Infrastructure.Persistence.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Core.Entities.Note>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Note> builder)
    {
        builder.ToTable(Core.Entities.Note.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired(false)
            .IsUnicode(true)
            .HasMaxLength(200);

        builder.HasMany(x => x.Files)
            .WithOne(x => x.Note)
            .HasForeignKey(x => x.NoteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}