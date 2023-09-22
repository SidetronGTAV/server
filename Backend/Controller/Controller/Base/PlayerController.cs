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
        Alt.OnClientRequestObject += OnClientRequestObject;
        Alt.OnClientDeleteObject += OnClientDeleteObject;
    }

    [ScriptEvent(ScriptEventType.PlayerConnect)]
    public void OnPlayerConnection(MyPlayer player, string reason)
    {
        PlayerHandler.HandlePlayerConnect(player);
    }

    [ScriptEvent(ScriptEventType.PlayerDisconnect)]
    public void OnPlayerDisconnect(MyPlayer player, string reason)
    {
        PlayerHandler.HandlePlayerDisconnect(player);
    }

    [AsyncScriptEvent(ScriptEventType.PlayerDead)]
    public async Task OnPlayerDeadAsync(MyPlayer player, IEntity killer, uint weapon)
    {
        if (player.IsInCharacterId == 0)
        {
            player.Spawn(player.Position);
            return;
        }

        await CharacterDbHandler.SetCharacterDeadAsync(player);
        player.Emit("Client:DeadHandler:Dead");
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