using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Common.Models.Base;

[Owned]
public class Position
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static implicit operator Vector3(Position position) => new(position.X, position.Y, position.Z);

    public static implicit operator AltV.Net.Data.Position(Position position) =>
        new(position.X, position.Y, position.Z);
}