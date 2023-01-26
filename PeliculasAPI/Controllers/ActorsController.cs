using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entities = await _context.Actors.ToListAsync();
            return _mapper.Map<List<ActorDTO>>(entities);
        }
        [HttpGet("{id}", Name = "obtainActors")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entity = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) 
            {
                return NotFound();
            }
            return _mapper.Map<ActorDTO>(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
        { 
            var entity = _mapper.Map<Actor>(actorCreationDTO);
            _context.Add(entity);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ActorDTO>(entity);
            return new CreatedAtRouteResult("obtainActors", new { id = entity.Id }, dto);
        }
        [HttpPut]
        public async Task<ActionResult> put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
        {
            var entity = _mapper.Map<Actor>(actorCreationDTO);
            entity.Id = id;
            _context.Entry(entity).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Actors.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                NotFound();
            }
            _context.Remove(new Actor() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
