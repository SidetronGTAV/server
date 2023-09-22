using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Enums;
using Common.Models;
using Controller.Handler.Base;
using DataAccess.DbHandler;

namespace Controller.Controller.Base;

public class ConsoleController : IScript
{
    [AsyncClientEvent("Server:Console:SpawnVehicle")]
    public static async Task OnSpawnVehicle(MyPlayer player, string vehicleName)
    {
        if (player.SupportLevel < SupportLevel.Admin)
        {
            return;
        }

        await AltAsync.CreateVehicle(vehicleName, new(player.Position.X + 1, player.Position.Y + 1, player.Position.Z),
            player.Rotation);
    }

    [ClientEvent("Server:Console:DeleteVehicle")]
    public static void OnDeleteVehicle(MyPlayer player, int radius)
    {
        if (player.SupportLevel < SupportLevel.Admin)
        {
            return;
        }

        if (player.Vehicle != null)
        {
            player.Vehicle.Destroy();
            return;
        }

        var callback = new FunctionCallback<IVehicle>(vehicle =>
        {
            if (!(vehicle.Position.Distance(player.Position) <= radius)) return;
            vehicle.Destroy();
        });
        Alt.ForEachVehicles(callback);
    }

    [ClientEvent("Server:Console:PlayerPosition")]
    public static void OnPlayerPosition(MyPlayer player)
    {
        Console.WriteLine($"Position {player.Name}: {player.Position}");
    }

    [ClientEvent("Server:Console:PlayerID")]
    public static void OnGetPlayerId(MyPlayer player)
    {
        player.Emit("Client:Console:PlayerID", player.Id);
    }

    [ClientEvent("Server:Console:TpToMe")]
    public static void OnPlayerTpToMe(MyPlayer player, uint id)
    {
        if (player.SupportLevel < SupportLevel.Supporter)
        {
            return;
        }

        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer.Exists)
        {
            targetPlayer.Position = player.Position;
        }
    }

    [ClientEvent("Server:Console:TpToPlayer")]
    public static void OnTpToPlayer(MyPlayer player, uint id)
    {
        if (player.SupportLevel < SupportLevel.Supporter)
        {
            return;
        }

        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer.Exists)
        {
            player.Position = targetPlayer.Position;
        }
    }

    [ClientEvent("Server:Console:GiveWeapon")]
    public static void OnGiveWeapon(MyPlayer player, string weaponName)
    {
        if (player.SupportLevel < SupportLevel.Admin)
        {
            return;
        }

        try
        {
            player.GiveWeapon(Alt.Hash(weaponName), 9999, true);
        }
        catch (Exception)
        {
            //Ignore
        }
    }

    [ClientEvent("Server:Console:Revive")]
    public static async Task OnPlayerRevive(MyPlayer player, uint? id)
    {
        if (player.SupportLevel < SupportLevel.Supporter)
        {
            return;
        }

        var targetPlayer = id != null ? (MyPlayer)Alt.GetPlayerById((uint)id) : player;
        await CharacterHandler.RevivePlayerAsync(targetPlayer);
    }

    [ClientEvent("Server:Console:Rotation")]
    public static void OnPlayerRotation(MyPlayer player)
    {
        Console.WriteLine($"Rotation {player.Name}: {player.Rotation}");
    }
}