using System.Reflection.Metadata.Ecma335;
using DatingApp.DataContext;
using DatingApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    public class UserController : BaseApiController
    {
        private AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null) return NotFound();

            return user;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
