using Common.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class VehicleDbHandler
{
    public static async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }
}