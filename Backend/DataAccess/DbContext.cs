using System.Text.Json;
using AltV.Net.Data;
using Common.Models;
using Common.Models.Base;
using Common.Models.Entity;
using Common.Models.UserStuff;
using Common.Models.UserStuff.CharacterSkin;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=altV;Username=altV;Password=Simba");
    }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Character> Characters { get; set; }

    public DbSet<CharacterSkin> CharacterSkins { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasOne(ch => ch.CharacterSkin)
            .WithOne(chSkin => chSkin.Character);
    }
}