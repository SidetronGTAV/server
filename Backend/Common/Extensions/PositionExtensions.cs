using AltV.Net.Data;

namespace Common.Extensions;

public static class PositionExtensions
{
    public static float Length(this Position pos)
    {
        if (pos is {X: 0, Y: 0, Z: 0 }) return 0f;
        return (float)Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y + pos.Z * pos.Z);
    }
}