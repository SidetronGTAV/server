using Common.Models.Base;
using Common.Models.UserStuff;

namespace Common.Models.Entity;

public class Vehicle: IdBase
{
    public VehicleData VehicleData { get; set; }
    
    public int CharacterId { get; set; }
    
    public Character Character { get; set; }
    
    public Position Position { get; set; }
    
    public Position Rotation { get; set; }
}