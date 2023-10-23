using Common.Models.Base;

namespace DataAccess.DbHandler;

public class LogDbHandler
{
    public static void Log(Log log)
    {
        using var db = new DbContext();
        db.Logs.Add(log);
        db.SaveChanges();
    }

    public static async Task LogAsync(Log log)
    {
        await using var db = new DbContext();
        db.Logs.Add(log);
        await db.SaveChangesAsync();
    }
}