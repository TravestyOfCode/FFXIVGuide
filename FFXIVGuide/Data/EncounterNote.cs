using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Data;

public class EncounterNote
{
    public int Id { get; set; }

    public required string Note { get; set; }

    public int Ordinal { get; set; }

    public int EncounterId { get; set; }

    public required Encounter Encounter { get; set; }
}

public class EncounterNoteConfiguration : IEntityTypeConfiguration<EncounterNote>
{
    public void Configure(EntityTypeBuilder<EncounterNote> builder)
    {
        builder.ToTable(nameof(EncounterNote));
    }
}
