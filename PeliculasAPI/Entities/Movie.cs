using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool onCinema { get; set; }

        public DateTime premiereDate { get; set; }

        public string Poster { get; set; }
    }
}
