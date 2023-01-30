using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicies;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string container = "actors";

        public ActorsController(ApplicationDbContext context, IMapper mapper,IFileStorage fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery]PaginationDTO paginationDTO)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParametersPagiantion(queryable, paginationDTO.AmountRegistersPerPage);
            var entities = await queryable.Pageing(paginationDTO).ToListAsync();
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

            if (actorCreationDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreationDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreationDTO.Photo.FileName);
                    entity.photo = await _fileStorage.SaveArchive(content, extension, container, actorCreationDTO.Photo.ContentType);
                }
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ActorDTO>(entity);
            return new CreatedAtRouteResult("obtainActors", new { id = entity.Id }, dto);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
        {
            var entity = _mapper.Map<Actor>(actorCreationDTO);
            entity.Id = id;
            _context.Entry(entity).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entityDB = await _context.Actors.FirstOrDefaultAsync(x=>x.Id==id);
            if (entityDB == null) 
            {
                return NotFound();
            }
            var entityDTO= _mapper.Map<ActorPatchDTO>(entityDB);

            patchDocument.ApplyTo(entityDTO, ModelState);

            var isvalid= TryValidateModel(entityDTO);
            if (!isvalid) 
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(entityDTO, entityDB);

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
