using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorPatchDTO
    {
        [Required]
        [StringLength(40)]
        public string name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
