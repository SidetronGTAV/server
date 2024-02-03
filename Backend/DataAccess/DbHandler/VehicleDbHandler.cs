using Common.Enums.Types;
using Common.Models;
using Common.Models.Entity.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class VehicleDbHandler
{
    public static async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }

    public static async Task<List<Vehicle>> GetAllVehicleFromGarageAsync(Garage garage)
    {
        await using var db = new DbContext();
        return await db.Vehicles.Where(v => v.Garage == garage && v.IsInGarage).ToListAsync();
    }

    public static async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
    {
        await using var db = new DbContext();
        var newVehicle = db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();
        return newVehicle.Entity;
    }

    public static async Task UpdateVehicleAsync(Vehicle vehicle)
    {
        await using var db = new DbContext();
        db.Vehicles.Update(vehicle);
        await db.SaveChangesAsync();
    }

    public static async Task<List<Vehicle>> GetAllUnparkedVehicleAsync()
    {
        await using var db = new DbContext();
        return await db.Vehicles.Where(v => !v.IsInGarage).ToListAsync();
    }
    
    public static void SaveVehiclePosition(MyVehicle vehicle)
    {
        var position = vehicle.Position;
        var rotation = vehicle.Rotation;
        using var db = new DbContext();
        var dbVehicle = db.Vehicles.FirstOrDefault(c => c.Id == vehicle.VehicleId);
        if (dbVehicle == null) return;
        dbVehicle.Position = new Common.Models.Base.Position { X = position.X, Y = position.Y, Z = position.Z };
        dbVehicle.Rotation = new Common.Models.Base.Position { X = rotation.Roll, Y = rotation.Pitch, Z = rotation.Yaw };
        dbVehicle.LastPositionChangedAt = DateTime.UtcNow;
        db.SaveChanges();
    }
}