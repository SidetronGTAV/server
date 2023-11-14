using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff;

[Owned]
public class Ban
{
    public string Reason { get; set; }
    
    public DateTime? At { get; set; }
    
    public DateTime? Until { get; set; }
    
    public int? CreatedById { get; set; }
    
    public Account CreatedBy { get; set; }
}