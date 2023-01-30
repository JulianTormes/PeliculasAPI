using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;
using PeliculasAPI.Servicios;
using System.ComponentModel;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string container = "movies";

        public MoviesController(ApplicationDbContext context, IMapper mapper,
            IFileStorage fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }
        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> Get()
        {
            var movies = await _context.Movies.ToListAsync();
            return _mapper.Map<List<MovieDTO>>(movies);
        }
        [HttpGet("{id}", Name = "obtainMovie")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie= await _context.Movies.SingleOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return _mapper.Map<MovieDTO>(movie);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]MovieCreationDTO movieCreationDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreationDTO);
            if (movieCreationDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreationDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreationDTO.Poster.FileName);
                    movie.Poster = await _fileStorage.SaveArchive(content, extension, container, movieCreationDTO.Poster.ContentType);
                }
            }
            _context.Add(movie);
            await _context.SaveChangesAsync();
            var movieDTO = _mapper.Map<MovieDTO>(movieCreationDTO);
            return new CreatedAtRouteResult("obtainMovie", new { id=movie.Id}, movieDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movieDB = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movieDB == null)
            {
                return NotFound();
            }
            movieDB = _mapper.Map(movieCreationDTO, movieDB);
            if (movieCreationDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreationDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreationDTO.Poster.FileName);
                    movieDB.Poster = await _fileStorage.EditArchive(content, extension, container,
                        movieDB.Poster,
                        movieCreationDTO.Poster.ContentType);
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MoviePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entityDB = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (entityDB == null)
            {
                return NotFound();
            }
            var entityDTO = _mapper.Map<MoviePatchDTO>(entityDB);

            patchDocument.ApplyTo(entityDTO, ModelState);

            var isvalid = TryValidateModel(entityDTO);
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
            var exist = await _context.Movies.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                NotFound();
            }
            _context.Remove(new Movie() { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
