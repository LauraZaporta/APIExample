using API.Data;
using API.DTOs;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAll()
        {
            var films = await _context.Films.ToListAsync();
            if (films.Count == 0) { return NotFound("El catàleg està buit"); }
            return Ok(films); //Tot bé al servidor -> retorna resposta
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> Get(int id)
        {
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == id);
            if (film == null) { return NotFound("Film no trobat"); }
            return Ok(film);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> Delete(int id)
        {
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == id);
            if (film == null) { return NotFound("Film no trobat"); }
            else { _context.Films.Remove(film); }
            return Ok($"Film {film.Name} deleted");
        }
        [HttpPut]
        public async Task<ActionResult<Film>> Update(FilmDTO filmDTO)
        {
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Name == filmDTO.Name);
            if (film == null) { return NotFound("Film a actualitzar no trobat"); }
            else {
                film.Year = filmDTO.Year;
                film.Name = filmDTO.Name;
            }
            _context.SaveChanges();

            return Ok($"Film {film.Name} updated");
        }
    }
}