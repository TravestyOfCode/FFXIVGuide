using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Data;

public class Encounter
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int Ordinal { get; set; }

    public int InstanceId { get; set; }

    public required Instance Instance { get; set; }

    public IEnumerable<EncounterNote>? EncounterNotes { get; set; }
}

public class EncounterConfiguration : IEntityTypeConfiguration<Encounter>
{
    public void Configure(EntityTypeBuilder<Encounter> builder)
    {
        builder.ToTable(nameof(Encounter));

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(128);

        builder.HasIndex(p => p.Name)
            .IsUnique(true)
            .IsClustered(false);
    }
}
