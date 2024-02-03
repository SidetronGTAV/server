using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Enums.Types;
using Common.Models;
using Common.Utilities.Positions;

namespace Controller.Controller.Base.Vehicle;

public class GarageController : IScript
{
    public static IColShape? LosSantosAirportColshape;

    public GarageController()
    {
        LosSantosAirportColshape = Alt.CreateColShapeCylinder(GaragePosition.LosSantosAirportColshapePosition, 3, 1.5f);
        LosSantosAirportColshape.SetData("Type", Garage.LosSantosAirport);
        Alt.OnColShape += OnColShape;
    }


    private static void OnColShape(IColShape colShape, IWorldObject targetEntity, bool state)
    {
        if (targetEntity is not MyPlayer myPlayer) return;
        if (myPlayer.IsInCharacterId == 0 || myPlayer.IsCharacterUnconscious) return;
        colShape.GetData("Type", out dynamic? type);
        if (type?.GetType() == typeof(Garage))
        {
            //TODO: Notify Player
        }
    }
}