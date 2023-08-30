﻿using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Models;

namespace Controller.Handler.Base;

public class ConsoleHandler: IScript
{
    [ClientEvent("Server:Console:SpawnVehicle")]
    public static void OnSpawnVehicle(MyPlayer player, string vehicleName)
    {
        Alt.CreateVehicle(vehicleName, new(player.Position.X +1, player.Position.Y + 1, player.Position.Z), player.Rotation);
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
}