using System.Runtime.CompilerServices;
using Common.Enums;
using Common.Enums.Logging;
using Common.Models.Base;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public abstract class LogHandler
{
    public static void Log(LogType type, LogSystemType systemType, string message, int? accountId = null,
        int? characterId = null, [CallerMemberName] string callerMemberName = null!)
    {
        var log = new Log
        {
            Type = type,
            System = systemType,
            CallerMemberName = callerMemberName,
            Message = message,
            Date = DateTime.Now,
            AccountId = accountId,
            CharacterId = characterId
        };
        LogDbHandler.Log(log);
    }

    public static async Task LogAsync(LogType type, LogSystemType systemType, string message, int? accountId = null,
        int? characterId = null, [CallerMemberName] string callerMemberName = null!)
    {
        var log = new Log
        {
            Type = type,
            System = systemType,
            CallerMemberName = callerMemberName,
            Message = message,
            Date = DateTime.Now,
            AccountId = accountId,
            CharacterId = characterId
        };
        await LogDbHandler.LogAsync(log);
    }
}