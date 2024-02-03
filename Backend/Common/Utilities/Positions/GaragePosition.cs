using System.Numerics;
using AltV.Net.Data;
using Common.Enums.Types;
using Position = AltV.Net.Data.Position;

namespace Common.Utilities.Positions;

public static class GaragePosition
{
    public static readonly List<List<Vector3>> LosSantosAirportSpawnPositions = new()
    {
        new List<Vector3>
        {
            new Position(-990.2474f, -2707.0212f, 13.407f),
            new Rotation(-0.0035f, 0.00150f, -0.4877f)
        },
        new List<Vector3>
        {
            new Position(-986.4697f, -2708.4426f, 13.407f),
            new Rotation(-0.00005f, 0.00008f, -0.421f)
        },
        new List<Vector3>
        {
            new Position(-983.62f, -2709.35f, 13.407f),
            new Rotation(0.00012f, -0.0035f, -0.3254f)
        },
        new List<Vector3>
        {
            new Position(-980.65f, -2710.50f, 13.407f),
            new Rotation(0.0046f, -0.0023f, -0.1320f)
        }
    };

    public static readonly Position LosSantosAirportColshapePosition = new(-980.17f, -2689.16f, 13.083f);

    public static readonly List<GaragePositionModel> Garages =
    [
        new GaragePositionModel
        {
            Garage = Garage.LosSantosAirport,
            SpawnPositions = LosSantosAirportSpawnPositions
        }
    ];
}