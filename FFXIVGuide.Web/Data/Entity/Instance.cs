using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace FFXIVGuide.Web.Data.Entity;

public class Instance
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public int RouletteTypeId { get; set; }

    public RouletteType RouletteType { get; set; }

    public IEnumerable<Encounter> Encounters { get; set; }
}

public class InstanceConfiguration : IEntityTypeConfiguration<Instance>
{
    public void Configure(EntityTypeBuilder<Instance> builder)
    {
        builder.ToTable(nameof(Instance));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(64);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.HasOne(p => p.RouletteType)
            .WithMany(p => p.Instances)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.RouletteTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Encounters)
            .WithOne(p => p.Instance)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
