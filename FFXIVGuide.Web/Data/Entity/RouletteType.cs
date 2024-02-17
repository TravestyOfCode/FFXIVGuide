using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFXIVGuide.Web.Data.Entity;

public class RouletteType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<Instance> Instances { get; set; } = new List<Instance>();
}

public class RouletteTypeConfiguration : IEntityTypeConfiguration<RouletteType>
{
    public void Configure(EntityTypeBuilder<RouletteType> builder)
    {
        builder.ToTable(nameof(RouletteType));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(32);

        builder.HasMany(p => p.Instances)
            .WithOne(p => p.RouletteType)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.RouletteTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.Name)
            .IsUnique(true);
    }
}
