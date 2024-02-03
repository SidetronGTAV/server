using Common.Models.Base;

namespace Common.Models.UserStuff;

public class Ban : IdBase
{
    public string Reason { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int CreatedById { get; set; }

    public Account CreatedBy { get; set; }
}