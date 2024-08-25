using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Data;

public class InstanceType
{
    public int Id { get; set; }

    public required string Name { get; set; }
}

public class InstanceTypeConfiguration : IEntityTypeConfiguration<InstanceType>
{
    public void Configure(EntityTypeBuilder<InstanceType> builder)
    {
        builder.ToTable(nameof(InstanceType));

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.HasIndex(p => p.Name)
            .IsUnique(true)
            .IsClustered(false);
    }
}
