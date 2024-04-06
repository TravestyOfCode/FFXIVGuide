using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuideAPI.Data;

internal class RouletteType
{
    /// <summary>
    /// The unique identifier for the RouletteType
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The display name for the RouletteType. Must be unique.
    /// </summary>
    public string Name { get; set; }

    public RouletteType()
    {
        Name = string.Empty;
    }
}

internal class RouletteTypeConfiguration : IEntityTypeConfiguration<RouletteType>
{
    public void Configure(EntityTypeBuilder<RouletteType> builder)
    {
        builder.ToTable(nameof(RouletteType));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.HasIndex(p => p.Name)
            .IsUnique(true)
            .IsClustered(false);
    }
}
