using System.Runtime.Versioning;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Events;
using Common.Models;
using Common.Models.Base;
using Controller.Handler.Base;
using DataAccess.DbHandler;
using Position = AltV.Net.Data.Position;

namespace Controller.Controller.Base;

public class PlayerController : IScript
{
    public PlayerController()
    {
        Alt.OnPlayerConnect += OnPlayerConnection;
        Alt.OnPlayerDisconnect += OnPlayerDisconnect;
        AltAsync.OnPlayerDead += OnPlayerDeadAsync;
        Alt.OnClientRequestObject += OnClientRequestObject;
        Alt.OnClientDeleteObject += OnClientDeleteObject;
    }

    private static void OnPlayerConnection(IPlayer player, string reason)
    {
        PlayerHandler.HandlePlayerConnect((MyPlayer)player);
    }

    private static void OnPlayerDisconnect(IPlayer player, string reason)
    {
        PlayerHandler.HandlePlayerDisconnect((MyPlayer)player);
    }

    private static async Task OnPlayerDeadAsync(IPlayer player, IEntity killer, uint weapon)
    {
        await PlayerHandler.HandlePlayerDead((MyPlayer)player, killer, weapon);
    }

    private static bool OnClientRequestObject(IPlayer target, uint model, Position position)
    {
        return model == (uint)WeaponModel.Parachute && target.HasWeapon(model);
    }

    private static bool OnClientDeleteObject(IPlayer target)
    {
        var myPlayer = (MyPlayer)target;
        return myPlayer.IsInCharacterId != 0;
    }
}