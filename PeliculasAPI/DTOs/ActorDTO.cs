﻿using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(40)]
        public string name { get; set; }
        public DateTime BirthDate { get; set; }
        public string photo { get; set; }
    }
}
