using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Models;

namespace Controller.Controller.Base;

public class ConsoleController : IScript
{
    [AsyncClientEvent("Server:Console:SpawnVehicle")]
    public static async Task OnSpawnVehicle(MyPlayer player, string vehicleName)
    {
        await AltAsync.CreateVehicle(vehicleName, new(player.Position.X + 1, player.Position.Y + 1, player.Position.Z),
            player.Rotation);
    }

    [ClientEvent("Server:Console:DeleteVehicle")]
    public static void OnDeleteVehicle(MyPlayer player, int radius)
    {
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
        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer.Exists)
        {
            targetPlayer.Position = player.Position;
        }
    }
    
    [ClientEvent("Server:Console:TpToPlayer")]
    public static void OnTpToPlayer(MyPlayer player, uint id)
    {
        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer.Exists)
        {
            player.Position = targetPlayer.Position;
        }
    }
    
    [ClientEvent("Server:Console:GiveWeapon")]
    public static void OnGiveWeapon(MyPlayer player, string weaponName)
    {
        try
        {
            player.GiveWeapon(Alt.Hash(weaponName), 9999, true);
        }
        catch (Exception)
        {
            //Ignore weil Samir stinkt
        }
        
    }
}