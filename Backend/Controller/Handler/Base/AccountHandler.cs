using Common.Models;
using Common.Models.Discord;
using Common.Models.UserStuff;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public abstract class AccountHandler
{
    public static async Task<Account> CreateAccountAsync(MyPlayer player, DiscordUser discordUser)
    {
        var cloudId = await player.RequestCloudId();
        var account = new Account()
        {
            Id = 0,
            DiscordId = discordUser.id,
            DiscordUsername = discordUser.username,
            HardwareIdHash = player.HardwareIdHash,
            HardwareIdExHash = player.HardwareIdExHash,
            SocialClubId = player.SocialClubId,
            MaxCharacters = 1,
            CloudId = cloudId
        };

        await AccountDbHandler.SaveAccountAsync(account);
        return account;
    }
}