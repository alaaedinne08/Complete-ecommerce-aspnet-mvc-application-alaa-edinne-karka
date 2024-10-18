﻿using eTickets.Data.Enums;

namespace eTickets.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageURL { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public int CinemaId { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorIds { get; set; }
    }
}
