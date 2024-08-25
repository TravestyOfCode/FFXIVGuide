using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<EncounterNote> EncounterNotes { get; set; }
    public DbSet<Instance> Instances { get; set; }
    public DbSet<InstanceType> InstanceTypes { get; set; }
    public DbSet<RouletteType> RouletteTypes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}
