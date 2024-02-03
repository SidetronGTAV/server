using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Common.Enums;
using Common.Enums.Types;
using Common.Models;
using Controller.Handler.Base;
using Controller.Handler.Base.CharacterStuff;
using Controller.Handler.Base.Vehicle;
using Controller.Utility.Callback;

namespace Controller.Controller.Base;

public class ConsoleController : IScript
{
    [AsyncClientEvent("Server:Console:SpawnVehicle")]
    public static async Task OnSpawnVehicle(MyPlayer player, string vehicleName)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;

        await AltAsync.CreateVehicle(vehicleName,
            new Position(player.Position.X + 1, player.Position.Y + 1, player.Position.Z),
            player.Rotation);
    }

    [ClientEvent("Server:Console:StartEngine")]
    public static void OnStartEngine(MyPlayer player)
    {
        if (player.SupportLevel < SupportLevel.Supporter) return;

        if (player.Vehicle == null) return;
        player.Vehicle.EngineOn = true;
    }

    [ClientEvent("Server:Console:DeleteVehicle")]
    public static void OnDeleteVehicle(MyPlayer player, int radius)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;

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

    [ClientEvent("Server:Console:PlayerID")]
    public static void OnGetPlayerId(MyPlayer player)
    {
        player.Emit("Client:Console:PlayerID", player.Id);
    }

    [ClientEvent("Server:Console:TpToMe")]
    public static void OnPlayerTpToMe(MyPlayer player, uint id)
    {
        if (player.SupportLevel < SupportLevel.Supporter) return;

        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer != null && targetPlayer.IsInCharacterId != 0) targetPlayer.Position = player.Position;
    }

    [ClientEvent("Server:Console:TpToPlayer")]
    public static void OnTpToPlayer(MyPlayer player, uint id)
    {
        if (player.SupportLevel < SupportLevel.Supporter) return;

        var targetPlayer = (MyPlayer)Alt.GetPlayerById(id);
        if (targetPlayer != null && targetPlayer.IsInCharacterId != 0) player.Position = targetPlayer.Position;
    }

    [ClientEvent("Server:Console:GiveWeapon")]
    public static void OnGiveWeapon(MyPlayer player, string weaponName)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;

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
        if (player.SupportLevel < SupportLevel.Supporter) return;

        var targetPlayer = id != null ? (MyPlayer)Alt.GetPlayerById((uint)id) : player;
        await CharacterHandler.ReviveCharacterAsync(targetPlayer);
        targetPlayer.Health = targetPlayer.MaxHealth;
    }

    [ClientEvent("Server:Console:Rotation")]
    public static void OnPlayerRotation(MyPlayer player)
    {
        Console.WriteLine($"Rotation {player.Name}: {player.Rotation}");
    }

    [AsyncClientEvent("Server:Console:BanPlayer")]
    public static async Task BanPlayerAsync(MyPlayer player, long discordId, string reason)
    {
        await BanHandler.BanAccountAsync(player, discordId, reason);
    }

    [ClientEvent("Server:Console:TpToPos")]
    public static void OnTpToPos(MyPlayer player, float x, float y, float z)
    {
        if (player.SupportLevel < SupportLevel.Supporter) return;

        player.Position = new Position(x, y, z);
    }

    [AsyncClientEvent("Server:Console:CreateDbVehicle")]
    public static async Task OnCreateDbVehicleAsync(MyPlayer player, string vehicleName)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;
        uint model = Alt.Hash(vehicleName);
        Common.Models.Base.Position position = new()
            { X = player.Position.X, Y = player.Position.Y, Z = player.Position.Z };
        await VehicleHandler.CreateNewVehicleAsync(player, model, position);
    }

    [AsyncClientEvent("Server:Console:VehicleIntoGarage")]
    public static async Task OnVehicleIntoGarageAsync(MyPlayer player, string garageName)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;

        var vehicle = (MyVehicle)player.Vehicle;

        await GarageHandler.VehicleIntoGarageAsync(vehicle, Enum.Parse<Garage>(garageName));
    }

    [AsyncClientEvent("Server:Console:VehicleOutOfGarage")]
    public static async Task OnVehicleOutOfGarageAsync(MyPlayer player, int vehicleId)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;
        await GarageHandler.VehicleOutOfGarageAsync(vehicleId);
    }

    [ClientEvent("Server:Console:RotationVehicle")]
    public static void OnRotationVehicle(MyPlayer player)
    {
        if (player.SupportLevel < SupportLevel.Admin) return;
        if (player.Vehicle == null) return;
        Console.WriteLine($"Rotation {player.Vehicle.Model}: {player.Vehicle.Rotation}");
    }
}