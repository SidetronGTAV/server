using Common.Models.Base;

namespace Common.Models.UserStuff;

public class BanHistory : IdBase
{
    public Account Account { get; set; }

    public string Reason { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime CreationDate { get; set; }

    public Account CreatedBy { get; set; }
}