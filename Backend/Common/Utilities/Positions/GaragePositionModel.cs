using System.Numerics;
using Common.Enums.Types;

namespace Common.Utilities.Positions;

public class GaragePositionModel
{
    public Garage Garage { get; set; }

    public List<List<Vector3>> SpawnPositions { get; set; }
}