using System.Threading.Channels;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Events;
using Common.Enums;
using Common.Models;
using Controller.Handler.Base.CharacterStuff;
using Controller.Utility;
using DataAccess;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public static class PlayerHandler
{
    public static void HandlePlayerConnect(MyPlayer player)
    {
        player.Dimension = DimensionHandler.GetPrivateDimension();
        player.Position = GlobalPosition.PlayerLoginPosition;
        player.Model = (uint)PedModel.FreemodeMale01;
        player.Frozen = true;
        player.SetDateTime(DateTime.Now);
    }

    public static void HandlePlayerDisconnect(MyPlayer player)
    {
        if (player.IsCharacterDead)
        {
            player.Position = GlobalPosition.HospitalSpawnPosition;
        }
        VoiceHandler.RemoveFromAllVoiceChannels(player);
        CharacterDbHandler.SaveCharacterPosition(player.IsInCharacterId, player.Position);
    }

    public static async Task HandlePlayerDead(MyPlayer player, IEntity killer, uint weapon)
    {
        if (player.IsInCharacterId == 0)
        {
            player.Spawn(player.Position);
            return;
        }

        await CharacterHandler.SetCharacterUnconsciousAsync(player);
        player.Emit("Client:DeadHandler:Dead");
    }
}