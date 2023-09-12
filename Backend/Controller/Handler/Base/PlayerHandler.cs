using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Events;
using Common.Models;
using Controller.Utility;
using DataAccess;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public static class PlayerHandler
{
    public static void HandlePlayerConnect(MyPlayer player)
    {
        player.Dimension = DimensionHandler.GetPrivateDimension();
        player.Position = new Position(-1562.5055f, -579.6528f, 108.50769f);
        player.Model = (uint)PedModel.FreemodeMale01;
        player.Frozen = true;
        player.SetDateTime(DateTime.Now);
    }
    
    public static void HandlePlayerDisconnect(MyPlayer player)
    {
        CharacterDbHandler.SaveCharacterPosition(player.isInCharacterId, player.Position);
    }
}