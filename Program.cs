using eTickets.Data; // Import your Data context
using eTickets.Data.Services;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container (equivalent to the ConfigureServices in Startup.cs)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
        //Services Configuration
        builder.Services.AddScoped<IActorsService, ActorsService>();

        builder.Services.AddControllersWithViews();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Seed the database
        AppDbInitializer.Seed(app);

        app.Run();
    }
}