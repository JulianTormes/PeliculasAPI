using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class MovieCreationDTO:MoviePatchDTO
    {
        [WeigthArchiveValidation(MaxWeigthInMB:4)]
        [ArchiveTypeValidation(GroupTypeArchive.Image)]
        public IFormFile Poster { get; set; }
    }
}
