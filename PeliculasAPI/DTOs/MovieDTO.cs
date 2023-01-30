using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool onCinema { get; set; }

        public DateTime premiereDate { get; set; }

        public string Poster { get; set; }
    }
}
