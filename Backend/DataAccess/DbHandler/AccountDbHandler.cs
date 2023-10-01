using AltV.Net;
using Common.Models;
using Common.Models.Base;
using Common.Models.Discord;
using Common.Models.UserStuff;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class AccountDbHandler
{
    public static async Task<Account?> GetAccountByDiscordIdAsync(long discordId)
    {
        await using var db = new DbContext();
        return await db.Accounts.Include(a => a.Characters.OrderBy(ch => ch.Id))
            .FirstOrDefaultAsync(x => x.DiscordId == discordId);
    }

    public static async Task<int> SaveAccountAsync(Account account)
    {
        await using var db = new DbContext();
        var result = account.Id == 0 ? db.Accounts.Add(account) : db.Accounts.Update(account);
        await db.SaveChangesAsync();
        return result.Entity.Id;
    }

    public static async Task<bool> FindOtherHardwareIdHashesSocialClubIdsAndCloudIdsAsync(Account account)
    {
        try
        {
            await using var db = new DbContext();
            var accounts = await db.Accounts.SingleOrDefaultAsync(a =>
                (a.HardwareIdHash == account.HardwareIdHash && a.HardwareIdExHash == account.HardwareIdExHash) ||
                a.SocialClubId == account.SocialClubId || a.CloudId == account.CloudId);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}