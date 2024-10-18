using eTickets.Data.Base;
using eTickets.Models;
using eTickets.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // 1. GetAllAsync Method: Fetch all movies with related data
        public async Task<IEnumerable<Movie>> GetAllAsync(Func<IQueryable<Movie>, IQueryable<Movie>> include = null)
        {
            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.Actors_Movies).ThenInclude(am => am.Actor);

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        // 2. GetMovieByIdAsync Method: Fetch a movie by ID with related data
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.Actors_Movies).ThenInclude(am => am.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // 3. GetNewMovieDropdownsValues Method: Fetch dropdown data for creating/editing a movie
        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync(),
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync()
            };

            return response;
        }

        // 4. AddNewMovieAsync Method: Add a new movie with related actors
        public async Task AddNewMovieAsync(NewMovieVM movie)
        {
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            // Add actors for this movie
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }

        // 5. UpdateMovieAsync Method: Update an existing movie with related actors
        public async Task UpdateMovieAsync(NewMovieVM movie)
        {
            // Fetch the movie from the database
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if (dbMovie == null) return;

            // Update movie fields
            dbMovie.Name = movie.Name;
            dbMovie.Description = movie.Description;
            dbMovie.Price = movie.Price;
            dbMovie.ImageURL = movie.ImageURL;
            dbMovie.StartDate = movie.StartDate;
            dbMovie.EndDate = movie.EndDate;
            dbMovie.CinemaId = movie.CinemaId;
            dbMovie.ProducerId = movie.ProducerId;

            await _context.SaveChangesAsync();

            // Update Actors_Movies relationship
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);

            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
