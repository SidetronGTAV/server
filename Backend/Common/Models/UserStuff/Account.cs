namespace Common.Models.Base;

public class Account : IdBase
{
    public string Username { get; set; }

    public long DiscordId { get; set; }

    public string DiscordUsername { get; set; }

    public ulong HardwareIdHash { get; set; }

    public ulong HardwareIdExHash { get; set; }

    public ulong SocialClubId { get; set; }
}