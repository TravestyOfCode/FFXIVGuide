using FFXIVGuide.Web.Data;
using FFXIVGuide.Web.Data.Encounter.Behaviors;
using FFXIVGuide.Web.Data.Instance.Behaviors;
using FFXIVGuide.Web.Data.Note.Behaviors;
using FFXIVGuide.Web.Data.RouletteType.Behaviors;
using FFXIVGuide.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FFXIVGuide.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDBContext>();


        builder.Services.AddControllersWithViews();

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            config.AddRouletteTypeBehaviors();
            config.AddInstanceBehaviors();
            config.AddEncounterBehaviors();
            config.AddNoteBehaviors();
        });

        // Add User Services
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserAccessor, HttpContextUserAccessor>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}
