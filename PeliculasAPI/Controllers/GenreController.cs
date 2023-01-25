using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var entities = await _context.Genres.ToListAsync();
            var dtos = _mapper.Map<List<GenreDTO>>(entities);
            return dtos;
        }
        [HttpGet("{id:int}", Name = "obtainGenre")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var entity = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<GenreDTO>(entity);
            return dto;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreatrionDTO genreCreatrionDTO)
        {
            var entity = _mapper.Map<Genre>(genreCreatrionDTO);
            _context.Add(entity);
            await _context.SaveChangesAsync();
            var genreDTO = _mapper.Map<GenreDTO>(entity);
            return new CreatedAtRouteResult("obtainGenre", new { id = genreDTO.Id }, genreDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]GenreCreatrionDTO genreCreatrionDTO)
        {
            var entity = _mapper.Map<Genre>(genreCreatrionDTO);
            entity.Id = id;
            _context.Entry(entity).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Genres.AnyAsync(x=>x.Id== id);
            if (!exist)
            {
                NotFound();
            }
            _context.Remove(new Genre() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
