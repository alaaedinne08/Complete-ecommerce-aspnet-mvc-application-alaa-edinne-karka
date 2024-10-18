using eTickets.Data.Enums;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();

                context.Database.EnsureCreated();

                // Seed roles
                await SeedRoles(roleManager);

                // Seed default admin user
                await SeedAdminUser(userManager);

                // Seed data for Cinemas
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema() { Name = "Cinema 1", Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg", Descreption = "This is the description of the first cinema" },
                        new Cinema() { Name = "Cinema 2", Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg", Descreption = "This is the description of the second cinema" },
                        new Cinema() { Name = "Cinema 3", Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg", Descreption = "This is the description of the third cinema" },
                        new Cinema() { Name = "Cinema 4", Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg", Descreption = "This is the description of the fourth cinema" },
                        new Cinema() { Name = "Cinema 5", Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg", Descreption = "This is the description of the fifth cinema" },
                    });
                    context.SaveChanges();
                }

                // Seed data for Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor() { FullName = "Actor 1", Bio = "This is the Bio of the first actor", ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-1.jpeg" },
                        new Actor() { FullName = "Actor 2", Bio = "This is the Bio of the second actor", ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-2.jpeg" },
                        new Actor() { FullName = "Actor 3", Bio = "This is the Bio of the third actor", ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-3.jpeg" },
                        new Actor() { FullName = "Actor 4", Bio = "This is the Bio of the fourth actor", ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-4.jpeg" },
                        new Actor() { FullName = "Actor 5", Bio = "This is the Bio of the fifth actor", ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-5.jpeg" }
                    });
                    context.SaveChanges();
                }

                // Seed data for Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer() { FullName = "Producer 1", Bio = "This is the Bio of the first producer", ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-1.jpeg" },
                        new Producer() { FullName = "Producer 2", Bio = "This is the Bio of the second producer", ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-2.jpeg" },
                        new Producer() { FullName = "Producer 3", Bio = "This is the Bio of the third producer", ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-3.jpeg" },
                        new Producer() { FullName = "Producer 4", Bio = "This is the Bio of the fourth producer", ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-4.jpeg" },
                        new Producer() { FullName = "Producer 5", Bio = "This is the Bio of the fifth producer", ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-5.jpeg" }
                    });
                    context.SaveChanges();
                }

                // Seed data for Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie() { Name = "Life", Description = "This is the Life movie description", Price = 39.50, ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10), CinemaId = 3, ProducerId = 3, MovieCategory = MovieCategory.Documentary },
                        new Movie() { Name = "The Shawshank Redemption", Description = "This is the Shawshank Redemption description", Price = 29.50, ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3), CinemaId = 1, ProducerId = 1, MovieCategory = MovieCategory.Action },
                        new Movie() { Name = "Ghost", Description = "This is the Ghost movie description", Price = 39.50, ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), CinemaId = 4, ProducerId = 4, MovieCategory = MovieCategory.Horror },
                        new Movie() { Name = "Race", Description = "This is the Race movie description", Price = 39.50, ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5), CinemaId = 1, ProducerId = 2, MovieCategory = MovieCategory.Documentary },
                        new Movie() { Name = "Scoob", Description = "This is the Scoob movie description", Price = 39.50, ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-2), CinemaId = 1, ProducerId = 3, MovieCategory = MovieCategory.Cartoon },
                        new Movie() { Name = "Cold Soles", Description = "This is the Cold Soles movie description", Price = 39.50, ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg", StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(20), CinemaId = 1, ProducerId = 5, MovieCategory = MovieCategory.Drama }
                    });
                    context.SaveChanges();
                }

                // Seed data for Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie() { ActorId = 1, MovieId = 1 },
                        new Actor_Movie() { ActorId = 3, MovieId = 1 },
                        new Actor_Movie() { ActorId = 1, MovieId = 2 },
                        new Actor_Movie() { ActorId = 4, MovieId = 2 },
                        new Actor_Movie() { ActorId = 1, MovieId = 3 },
                        new Actor_Movie() { ActorId = 2, MovieId = 3 },
                        new Actor_Movie() { ActorId = 5, MovieId = 3 },
                        new Actor_Movie() { ActorId = 2, MovieId = 4 },
                        new Actor_Movie() { ActorId = 3, MovieId = 4 },
                        new Actor_Movie() { ActorId = 4, MovieId = 4 },
                        new Actor_Movie() { ActorId = 2, MovieId = 5 },
                        new Actor_Movie() { ActorId = 3, MovieId = 5 },
                        new Actor_Movie() { ActorId = 4, MovieId = 5 },
                        new Actor_Movie() { ActorId = 5, MovieId = 5 },
                        new Actor_Movie() { ActorId = 3, MovieId = 6 },
                        new Actor_Movie() { ActorId = 4, MovieId = 6 },
                        new Actor_Movie() { ActorId = 5, MovieId = 6 },
                    });
                    context.SaveChanges();
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin role if it doesn't exist
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        private static async Task SeedAdminUser(UserManager<IdentityUser> userManager)
        {
            const string adminEmail = "admin@example.com"; // Change this to your desired admin email
            const string adminPassword = "Admin@123"; // Change this to your desired password

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Set to true if you want to bypass email confirmation
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign Admin role to the new user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
