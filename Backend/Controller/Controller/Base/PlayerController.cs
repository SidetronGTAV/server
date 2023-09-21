using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Models;
using Controller.Handler.Base;
using DataAccess.DbHandler;

namespace Controller.Controller.Base;

public class PlayerController : IScript
{
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
}