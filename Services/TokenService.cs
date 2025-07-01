using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DatingApp.Entities;
using DatingApp.interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Services;

public class TokenService : ITokenService
{
    private IConfiguration _config { get; set; }
    public TokenService(IConfiguration config)
    {
        _config = config;
    }
    public string CreateToken(AppUser user)
    {
        var TokenKey = _config["TokenKey"] ?? throw new Exception("Cannot acces The token");
        if (TokenKey.Length < 64) throw new Exception("Token Length must be Greater Than 64");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));

        var claims = new List<Claim>
        {
                new Claim(ClaimTypes.NameIdentifier,user.userName)
        };
        var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = Credentials

        };
        var tokenHandle = new JwtSecurityTokenHandler();
        var token = tokenHandle.CreateToken(tokenDescription);
        return tokenHandle.WriteToken(token);
    }
}
