using AltV.Net;
using AltV.Net.Async;
using Common.Enums.Types;
using Common.Models;
using Controller.Controller.Base.Vehicle;
using Controller.Handler.Base.Vehicle;

namespace Controller.Controller.Base;

public class ControlHandler : IScript
{
    public ControlHandler()
    {
        AltAsync.OnClient<MyPlayer, Task>("Server:Control:PressedE", PlayerPressedE);
    }

    private static async Task PlayerPressedE(MyPlayer player)
    {
        if (player.IsInCharacterId == 0 || player.IsCharacterUnconscious) return;
        if (GarageController.LosSantosAirportColshape!.IsEntityIn(player) && player.Vehicle == null)
            await GarageHandler.PlayerEnteredGaragePointAsync(player, Garage.LosSantosAirport);
    }
}