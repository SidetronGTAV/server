using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Events;
using Common.Models;
using DataAccess;

namespace Controller.Handler.Base;

public static class PlayerConnectHandler
{
    public static void HandlePlayerConnect(MyPlayer player, string reason)
    {
        player.SetDateTime(DateTime.Now);
        player.Model = (uint)PedModel.FreemodeMale01;
        player.Spawn(new Position(199, -934, 31), 0);
        player.Emit("Client:Login:OpenLogin");
        Console.WriteLine("Player Connected");
    }
}