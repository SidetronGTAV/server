using AltV.Net;
using Common.Models;
using Common.Models.UserStuff;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public class BanHandler
{
    public static async Task<bool> IsPlayerBannedAsync(Account account)
    {
        if (account.Ban == null) return false;
        if (account.Ban.ExpirationDate == null || account.Ban.ExpirationDate >= DateTime.Now) return true;
        await RemoveBanAsync(account);
        return false;
    }
    
    private static async Task RemoveBanAsync(Account account)
    {
        BanHistory history = new()
        {
            Account = account,
            Reason = account.Ban.Reason,
            ExpirationDate = account.Ban.ExpirationDate,
            CreationDate = account.Ban.CreationDate,
            CreatedBy = account.Ban.CreatedBy
        };
        
        await BanDbHandler.DeleteBanAsync(account.Ban);
        account.Ban = null;
        account.BanId = null;
        account.BanHistory.Add(history);
        await AccountDbHandler.SaveAccountAsync(account);
        
    }

    public static async Task BanAccount(MyPlayer player, long discordId, string reason)
    {
        if (player.AccountId == null) return;
        var account = await AccountDbHandler.GetAccountByDiscordIdAsync(discordId);
        if (account == null) return;
        account.Ban = new Ban
        {
            Reason = reason,
            CreationDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddMinutes(10),
            CreatedById = player.AccountId.Value
        };
        await AccountDbHandler.SaveAccountAsync(account);
        var target = PlayerHandler.FindPlayerByDiscordId(discordId);
        target?.Kick($"Du wurdest von unserem Team gebannt! Grund: {reason}! Dein Bann läuft {account.Ban.ExpirationDate} ab!");

    }
}