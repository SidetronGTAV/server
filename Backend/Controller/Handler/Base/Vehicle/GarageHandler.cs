using System.Numerics;
using AltV.Net;
using Common.Enums.Types;
using Common.Models;
using Common.Models.Base;
using Common.Utilities.Positions;
using Controller.Utility;
using DataAccess.DbHandler;
using EntityType = AltV.Net.Data.EntityType;

namespace Controller.Handler.Base.Vehicle;

public static class GarageHandler
{
    public static async Task PlayerEnteredGaragePointAsync(MyPlayer player, Garage garage)
    {
        var vehicles = await VehicleDbHandler.GetAllVehicleFromGarageAsync(garage);
        player.Emit("Client:Garage:ShowGarageMenu", vehicles);
    }

    public static async Task VehicleIntoGarageAsync(MyVehicle vehicle, Garage garage)
    {
        var dbVehicle = await VehicleDbHandler.GetVehicleByIdAsync(vehicle.VehicleId);
        if (dbVehicle is null) return;
        dbVehicle.Garage = garage;
        dbVehicle.IsInGarage = true;
        await VehicleDbHandler.UpdateVehicleAsync(dbVehicle);
        vehicle.Destroy();
    }

    public static async Task VehicleOutOfGarageAsync(int vehicleId)
    {
        var dbVehicle = await VehicleDbHandler.GetVehicleByIdAsync(vehicleId);
        if (dbVehicle is null || !dbVehicle.IsInGarage) return;

        var garage = GaragePosition.Garages.Find(g => g.Garage == dbVehicle.Garage);
        if (garage == null) return;

        var spawnPositions = FindSpawnablePositionInGarageZone(garage);
        if (spawnPositions is not { Count: 2 })
            //TODO: Notify Player
            return;

        var vehicle = (MyVehicle)Alt.CreateVehicle(dbVehicle.Model, spawnPositions[0], spawnPositions[1]);
        VehicleHandler.SetVehicleData(vehicle, dbVehicle);

        dbVehicle.IsInGarage = false;
        dbVehicle.Garage = null;
        dbVehicle.Position = new Position() {X = vehicle.Position.X, Y = vehicle.Position.Y, Z = vehicle.Position.Z};
        dbVehicle.LastPositionChangedAt = DateTime.UtcNow;
        await VehicleDbHandler.UpdateVehicleAsync(dbVehicle);
    }

    private static List<Vector3>? FindSpawnablePositionInGarageZone(GaragePositionModel garage)
    {
        return garage.SpawnPositions.FirstOrDefault(s =>
            Alt.Core.GetClosestEntities(s[0], 2, DimensionHandler.DefaultDimension, 1, EntityType.Vehicle).Length == 0);
    }
}