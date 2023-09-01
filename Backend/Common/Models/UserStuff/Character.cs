using Common.Models.Base;

namespace Common.Models.UserStuff;

public class Character: IdBase
{
    public string Firstname { get; set; }
    
    public string Lastname { get; set; }
    
    public Account Account { get; set; }
}