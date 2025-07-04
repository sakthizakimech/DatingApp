using System;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.interfaces;

namespace DatingApp.ServiceExtentions;

public static class AppUserExtensions
{
public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }
}
