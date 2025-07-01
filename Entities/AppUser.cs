using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Entities;

public class AppUser
{
    public int id { get; set; }
    public required string userName { get; set; }
    public required byte[] PasswordHash{ get;  set;}
    public required byte[] PasswordSalt{ get;  set;}
}
