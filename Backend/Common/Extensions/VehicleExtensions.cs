using AltV.Net.Elements.Entities;

namespace Common.Extensions;

public static class VehicleExtensions
{
    public static float GetSpeed(this IVehicle vehicle)
    {
        return vehicle.Velocity.Length() * 3.6f;
    }
}