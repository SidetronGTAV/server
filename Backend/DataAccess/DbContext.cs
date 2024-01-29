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
    
    public DbSet<Ban> Bans { get; set; }
    
    public DbSet<BanHistory> BanHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasOne(ch => ch.CharacterSkin)
            .WithOne(chSkin => chSkin.Character);

        modelBuilder.Entity<Account>()
            .HasMany<BanHistory>(a => a.BanHistory)
            .WithOne(b => b.Account)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .HasOne<Ban>(a => a.Ban)
            .WithOne()
            .HasForeignKey<Account>(a => a.BanId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Ban>()
            .HasOne<Account>(b => b.CreatedBy)
            .WithMany()
            .HasForeignKey(b => b.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}