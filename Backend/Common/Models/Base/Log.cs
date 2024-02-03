using Common.Enums;
using Common.Enums.Logging;
using Common.Models.UserStuff;

namespace Common.Models.Base;

public class Log : IdBase
{
    public DateTime Date = DateTime.UtcNow;
    public LogType Type { get; set; }

    public LogSystemType System { get; set; }

    public string CallerMemberName { get; set; }

    public string Message { get; set; }

    public int? AccountId { get; set; }

    public Account Account { get; set; }

    public int? CharacterId { get; set; }

    public Character Character { get; set; }
}