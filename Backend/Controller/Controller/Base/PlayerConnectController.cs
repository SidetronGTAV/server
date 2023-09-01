using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Models;
using Controller.Handler.Base;

namespace Controller.Controller.Base;

public class PlayerConnectController : IScript
{
    [ScriptEvent(ScriptEventType.PlayerConnect)]
    public void OnPlayerConnection(MyPlayer player, string reason)
    {
        PlayerConnectHandler.HandlePlayerConnect(player);
    }
}