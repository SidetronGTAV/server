using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Models;
using Controller.Handler.Base;

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
}