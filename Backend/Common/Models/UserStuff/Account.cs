using Common.Enums;
using Common.Models.Base;

namespace Common.Models.UserStuff;

public class Account : IdBase
{
    public long DiscordId { get; set; }

    public string DiscordUsername { get; set; }

    public ulong HardwareIdHash { get; set; }

    public ulong HardwareIdExHash { get; set; }

    public ulong SocialClubId { get; set; }

    public string CloudId { get; set; }

    public SupportLevel SupportLevel { get; set; }

    public bool Whitelisted { get; set; }

    public DateTime? WhitelistedAt { get; set; }

    public List<Character> Characters { get; set; }

    public int MaxCharacters { get; set; }
    
}