using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace FFXIVGuide.Web.Data;

public class Encounter
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public ApplicationUser Owner { get; set; }

    public int InstanceId { get; set; }

    public Instance Instance { get; set; }

    public string Name { get; set; }

    public int Ordinal { get; set; }

    public IEnumerable<Note> Notes { get; set; }
}

public class EncounterConfiguration : IEntityTypeConfiguration<Encounter>
{
    public void Configure(EntityTypeBuilder<Encounter> builder)
    {
        builder.ToTable(nameof(Encounter));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.OwnerId)
            .IsRequired(false);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.HasOne(p => p.Owner)
            .WithMany()
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Notes)
            .WithOne(p => p.Encounter)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.EncounterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Instance)
            .WithMany(p => p.Encounters)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.InstanceId);
    }
}
