using Common.Models;
using Common.Models.Discord;
using Common.Models.UserStuff;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public abstract class AccountHandler
{
    public static async Task<Account> CreateAccountAsync(MyPlayer player, DiscordUser discordUser)
    {
        var altDiscordAccount = await AccountDbHandler.GetAccountByDiscordIdAsync(player.DiscordId);

        if (altDiscordAccount != null)
        {
            player.Kick(
                "Dein Discord Account wo du dich gerade eingeloggt hast, ist mit nicht der selbe wie der wo du dich das letzte mal eingeloggt hast! Bitte Wende dich an den Support!");
            return null;
        }

        var account = new Account()
        {
            Id = 0,
            DiscordId = discordUser.id,
            DiscordUsername = discordUser.username,
            HardwareIdHash = player.HardwareIdHash,
            HardwareIdExHash = player.HardwareIdExHash,
            SocialClubId = player.SocialClubId,
            MaxCharacters = 1
        };

        await AccountDbHandler.SaveAccountAsync(account);
        return account;
    }
}