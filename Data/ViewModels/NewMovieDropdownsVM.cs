using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
            Actors = new List<Actor>();
        }

        public List<Cinema> Cinemas { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
