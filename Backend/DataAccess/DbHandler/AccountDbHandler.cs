using Common.Models.Base;
using Common.Models.Discord;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class AccountDbHandler
{
    public static async Task<Account?> GetAccountByDiscordIdAsync(long discordId)
    {
        await using var db = new DbContext();
        return await db.Accounts.FirstOrDefaultAsync(x => x.DiscordId == discordId);
    }

    public static async Task<Account> CreateAccountAsync(DiscordUser discordUser)
    {
        var account = new Account()
        {
            DiscordId = discordUser.id,
            DiscordUsername = discordUser.username
        };

        await using var db = new DbContext();
        await db.Accounts.AddAsync(account);
        await db.SaveChangesAsync();

        return account;
    }
}