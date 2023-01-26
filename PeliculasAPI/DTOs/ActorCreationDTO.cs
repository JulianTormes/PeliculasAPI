using Microsoft.EntityFrameworkCore.Storage;
using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [StringLength(40)]
        public string name { get; set; }
        public DateTime BirthDate { get; set; }
        [WeigthArchiveValidation(MaxWeigthInMB: 4)]
        [ArchiveTypeValidation(groupTypeArchive: GroupTypeArchive.Image)]
        public IFormFile Photo { get; set; }
    }
}
