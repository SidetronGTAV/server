using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=altV;Username=altV;Password=Simba");
    } 
    
    public DbSet<MyPlayer> Players { get; set; }
}