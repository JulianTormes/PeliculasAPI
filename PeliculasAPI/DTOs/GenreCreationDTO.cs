using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class GenreCreatrionDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
