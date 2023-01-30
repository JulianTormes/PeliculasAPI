using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class GenreDTO : GenreCreationDTO
    {
        public string Id { get; set; }
    }
}
