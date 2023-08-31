using Common.Models;
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

    public static async Task<Account> CreateAccountAsync(MyPlayer player, DiscordUser discordUser)
    {
        var altDiscordAccount = await GetAccountByDiscordIdAsync(player.DiscordId);

        if (altDiscordAccount != null)
        {
            player.Kick(
                "Dein Discord Account wo du dich gerade eingeloggt hast, ist mit nicht der selbe wie der wo du dich das letzte mal eingeloggt hast! Bitte Wende dich an den Support!");
            return null;
        }

        var account = new Account()
        {
            DiscordId = discordUser.id,
            DiscordUsername = discordUser.username,
            HardwareIdHash = player.HardwareIdHash,
            HardwareIdExHash = player.HardwareIdExHash,
            SocialClubId = player.SocialClubId,
        };

        await using var db = new DbContext();
        await db.Accounts.AddAsync(account);
        await db.SaveChangesAsync();

        return account;
    }
}