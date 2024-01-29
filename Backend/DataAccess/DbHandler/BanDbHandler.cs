using Common.Models.UserStuff;

namespace DataAccess.DbHandler;

public static class BanDbHandler
{
    public static async Task DeleteBanAsync(Ban ban)
    {
        await using var db = new DbContext();
        db.Bans.Remove(ban);
        await db.SaveChangesAsync();
    }
}