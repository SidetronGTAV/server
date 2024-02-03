using System.Numerics;
using AltV.Net.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Models.Base;

[Owned]
public class Position
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static implicit operator Vector3(Position position)
    {
        return new Vector3(position.X, position.Y, position.Z);
    }

    public static implicit operator AltV.Net.Data.Position(Position position)
    {
        return new AltV.Net.Data.Position(position.X, position.Y, position.Z);
    }

    public static implicit operator Rotation(Position position)
    {
        return new Rotation(position.X, position.Y, position.Z);
    }
}