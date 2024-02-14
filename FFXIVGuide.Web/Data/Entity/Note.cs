using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Web.Data.Entity;

public class Note
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public ApplicationUser Owner { get; set; }

    public int EncounterId { get; set; }

    public Encounter Encounter { get; set; }

    public int Ordinal { get; set; }

    public string Description { get; set; }
}

public class NoteConfiguraiton : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable(nameof(Note));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.OwnerId)
            .IsRequired(false);

        builder.Property(p => p.Description)
            .IsRequired(true);

        builder.HasOne(p => p.Owner)
            .WithMany()
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Encounter)
            .WithMany(p => p.Notes)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.EncounterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.EncounterId);
    }
}