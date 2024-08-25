using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Data;

public class RouletteType
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required IEnumerable<Instance> Instances { get; set; }
}

public class RouletteTypeConfiguration : IEntityTypeConfiguration<RouletteType>
{
    public void Configure(EntityTypeBuilder<RouletteType> builder)
    {
        builder.ToTable(nameof(RouletteType));

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.HasIndex(p => p.Name)
            .IsUnique(true)
            .IsClustered(false);
    }
}
