namespace Common.Models.Base;

public class Account : IdBase
{
    public string Username { get; set; }
    
    public long DiscordId { get; set; }
    
    public string DiscordUsername { get; set; }
}