using API.Data;
using API.DTOs;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class FilmsSyncController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FilmsSyncController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Film>> GetAll()
        {
            var films = _context.Films.ToList();
            if (films.Count == 0) { return NotFound("El catàleg està buit"); }
            return Ok(films); //Tot bé al servidor -> retorna resposta
        }

        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            var film = _context.Films.FirstOrDefault(x => x.Id == id);
            if (film == null) { return NotFound("Film no trobat"); }
            return Ok(film);
        }
        [HttpDelete("{id}")]
        public ActionResult<Film> Delete(int id)
        {
            var film = _context.Films.FirstOrDefault(x => x.Id == id);
            if (film == null) { return NotFound("Film no trobat"); }
            else { _context.Films.Remove(film); }
            return Ok($"Film {film.Name} deleted");
        }
        [HttpPost]
        public ActionResult<Film> Add(FilmDTO filmDTO)
        {
            var film = new Film { Name = filmDTO.Name, Year = filmDTO.Year };
            _context.Films.Add(film);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetAll), film);
        }
    }
}