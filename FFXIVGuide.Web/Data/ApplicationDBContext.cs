using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}
