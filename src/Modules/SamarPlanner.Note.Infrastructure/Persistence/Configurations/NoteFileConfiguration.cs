using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamarPlanner.Note.Core.Entities;

namespace SamarPlanner.Note.Infrastructure.Persistence.Configurations;

public class NoteFileConfiguration : IEntityTypeConfiguration<NoteFile>
{
    public void Configure(EntityTypeBuilder<NoteFile> builder)
    {
        builder.ToTable(NoteFile.TableName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.NoteId).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired(false)
            .IsUnicode(true)
            .HasMaxLength(200);

        builder.Property(x => x.Extension)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(10);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.Lenght)
            .IsRequired();

        builder.Ignore(x => x.FileName);
    }
}