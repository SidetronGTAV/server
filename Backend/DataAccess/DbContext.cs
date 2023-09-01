using Common.Models;
using Common.Models.Base;
using Common.Models.UserStuff;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=altV;Username=altV;Password=Simba");
    }

    public DbSet<Account> Accounts { get; set; }
}