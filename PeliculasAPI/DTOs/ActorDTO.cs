using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
    }
}
