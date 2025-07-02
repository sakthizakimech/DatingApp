using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using DatingApp.DataContext;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.interfaces;
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
            var User = await _context.users.FirstOrDefaultAsync(x => x.userName == info.UserName.ToLower());
            if (User == null) return Unauthorized("Invalid User Name");
            using var hasher = new HMACSHA512(User.PasswordSalt);
            var HashedPassword = hasher.ComputeHash(Encoding.UTF8.GetBytes(info.Password));
            for (int i = 0; i < HashedPassword.Length; i++)
            {

                if (HashedPassword[i] != User.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName = User.userName,
                Token = _service.CreateToken(User)
            };

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto info)
        {
            if (await isExist(info.UserName)) return BadRequest("User Name is Alread Taken");
            using var hasher = new HMACSHA512();

            var user = new AppUser
            {
                userName = info.UserName,
                PasswordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(info.Password)),
                PasswordSalt = hasher.Key
            };
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.userName,
                Token = _service.CreateToken(user)
            };

        }
        private async Task<bool> isExist(string userName)
        {
            return await _context.users.AnyAsync(x => x.userName.ToLower() == userName);
        }
    }
}
