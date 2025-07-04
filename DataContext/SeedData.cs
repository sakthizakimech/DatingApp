using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.Dtos;
using DatingApp.Entities;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataContext;

public class SeedData
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.users.AnyAsync()) return;
        var userData = await File.ReadAllTextAsync("DataContext/UserData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<List<SeedUserDto>>(userData, options);
        if (users == null) return;
        foreach (var user in users)
        {
            using var hasher = new HMACSHA512();
            
            var User = new AppUser
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                ImageUrl = user.ImageUrl,
                PasswordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hasher.Key,
                Member = new Member
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Description = user.Description,
                    DateOfBirth = user.DateOfBirth,
                    ImageUrl = user.ImageUrl,
                    Gender = user.Gender,
                    City = user.City,
                    Country = user.Country,
                    LastActive = user.LastActive,
                    Created = user.Created
                }
            };

            User.Member.Photos.Add(new Photo
            {
                Url = user.ImageUrl!,
                MemberId = user.Id
            });
            context.users.Add(User);
        }
        await context.SaveChangesAsync();
    }
}
