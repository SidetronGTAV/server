using AltV.Net.Elements.Entities;
using Common.Enums.Types;

namespace Common.Utilities.Positions;

public class GarageColShape
{
    public Garage Garage { get; set; }

    public List<IColShape> ColShapes { get; set; }
}