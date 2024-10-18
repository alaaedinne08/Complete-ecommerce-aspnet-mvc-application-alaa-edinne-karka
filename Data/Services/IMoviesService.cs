using eTickets.Data.ViewModels;
using eTickets.Models;

public interface IMoviesService
{
    Task<IEnumerable<Movie>> GetAllAsync(Func<IQueryable<Movie>, IQueryable<Movie>> include = null);
    Task<Movie> GetMovieByIdAsync(int id);
    Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
    Task AddNewMovieAsync(NewMovieVM movie);
    Task UpdateMovieAsync(NewMovieVM movie);  // Ensure this signature exists
}

