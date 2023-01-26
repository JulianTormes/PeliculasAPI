using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreatrionDTO, Genre>();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>().ForMember(x => x.photo, options => options.Ignore());
            CreateMap<ActorPatchDTO,Actor>().ReverseMap();
        }
    }
}
