using System;
using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<AppUser> users { get; set; }
}
