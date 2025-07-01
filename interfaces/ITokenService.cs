using System;
using DatingApp.Entities;

namespace DatingApp.interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
