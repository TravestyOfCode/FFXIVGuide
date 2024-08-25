using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Data;

public class Instance
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageLocation { get; set; }

    public int InstanceTypeId { get; set; }

    public required InstanceType InstanceType { get; set; }

    public IEnumerable<RouletteType>? RouletteTypes { get; set; }
}

public class InstanceConfiguration : IEntityTypeConfiguration<Instance>
{
    public void Configure(EntityTypeBuilder<Instance> builder)
    {
        builder.ToTable(nameof(Instance));

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(128);

        builder.Property(p => p.Description)
            .IsRequired(false);

        builder.Property(p => p.ImageLocation)
            .IsRequired(false);

        builder.HasIndex(p => p.Name)
            .IsUnique(true)
            .IsClustered(false);
    }
}
