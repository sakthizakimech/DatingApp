using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using DatingApp.DataContext;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.interfaces;
using DatingApp.ServiceExtentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        readonly AppDbContext _context;
        readonly ITokenService _service;
        public AccountController(AppDbContext context, ITokenService service)
        {
            _context = context;
            _service = service;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto info)
        {
            var User = await _context.users.FirstOrDefaultAsync(x => x.Email == info.Email);
            if (User == null) return Unauthorized("Invalid User Name");
            using var hasher = new HMACSHA512(User.PasswordSalt);
            var HashedPassword = hasher.ComputeHash(Encoding.UTF8.GetBytes(info.Password));
            for (int i = 0; i < HashedPassword.Length; i++)
            {

                if (HashedPassword[i] != User.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }
            return User.ToDto(_service);

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto info)
        {
            if (await EmailExists(info.Email)) return BadRequest("Email taken");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = info.DisplayName,
            Email = info.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(info.Password)),
            PasswordSalt = hmac.Key
        };

        _context.users.Add(user);
        await _context.SaveChangesAsync();

        return user.ToDto(_service);
            

        }
        private async Task<bool> EmailExists(string Mailid)
        {
            return await _context.users.AnyAsync(x => x.Email.ToLower() == Mailid);
        }
    }
}
