using Common.Enums.Types;
using Common.Models.Base;
using Common.Models.UserStuff;

namespace Common.Models.Entity.Vehicle;

public class Vehicle : IdBase
{
    public VehicleData VehicleData { get; set; }

    public uint Model { get; set; }

    public int CharacterId { get; set; }

    public Character Character { get; set; }

    public Position Position { get; set; }

    public Position Rotation { get; set; }

    public Garage? Garage { get; set; }

    public bool IsInGarage { get; set; }
    
    public DateTime LastPositionChangedAt { get; set; }
}