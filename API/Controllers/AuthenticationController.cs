using API.Data;
using API.DTOs;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO userDTO)
        {
            if (_context.Users.FirstOrDefault(u => u.Email == userDTO.Email) != null)
            {
                return BadRequest("User already exists");
            }
            var newUser = new User();
            var hashedPass = new PasswordHasher<User>()
                .HashPassword(newUser, userDTO.Password);
            newUser.Email = userDTO.Email;
            newUser.HashedPassword = hashedPass;
            newUser.Name = userDTO.Name;

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }
    }
}
