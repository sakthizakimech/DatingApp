using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace DatingApp.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public string DisplayName { get; set; } = "";
    [Required]
    [MinLength(6)]

    public required string Password { get; set; }
}
