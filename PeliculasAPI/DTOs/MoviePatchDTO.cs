using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class MoviePatchDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool onCinema { get; set; }

        public DateTime premiereDate { get; set; }
    }
}
