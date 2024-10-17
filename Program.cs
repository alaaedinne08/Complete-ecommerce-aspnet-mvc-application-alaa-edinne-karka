using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container (equivalent to the ConfigureServices in Startup.cs)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

        // Add Identity services
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Configure identity options
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        // Services Configuration
        builder.Services.AddScoped<IActorsService, ActorsService>();
        builder.Services.AddScoped<IProducersService, ProducersService>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();


        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages(); // Important for Razor pages, including Identity

        var app = builder.Build();

        // Configure the HTTP request pipeline (equivalent to the Configure method in Startup.cs)
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Add Authentication & Authorization middleware
        app.UseAuthentication(); // Enables authentication
        app.UseAuthorization();  // Enables authorization

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages(); // This is essential for Identity pages

        // Seed the database
        AppDbInitializer.Seed(app);

        app.Run();
    }
}
