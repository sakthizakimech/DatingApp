using System;
using Microsoft.AspNetCore.SignalR;

namespace DatingApp.Dtos;

public class RegisterDto
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
