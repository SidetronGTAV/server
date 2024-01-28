using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Models.Entity;
using DataAccess.DbHandler;

namespace Controller.Handler.Base.Vehicle;

public class VehicleHandler
{
    public static async Task<IVehicle> CreateVehicleAsync(uint model, int vehicleId)
    {
        var dbVehicle = await VehicleDbHandler.GetVehicleByIdAsync(vehicleId);
        if (dbVehicle is null) return null;
        
        var vehicle = Alt.CreateVehicle(model, dbVehicle.Position, dbVehicle.Rotation);
        SetVehicleData(vehicle, dbVehicle.VehicleData);
        return vehicle;
    }

    private static void SetVehicleData(IVehicle vehicle, VehicleData vehicleData)
    {
        vehicle.HealthData = vehicleData.HealthData;
        vehicle.DamageData = vehicleData.DamageData;
        vehicle.AppearanceData = vehicleData.AppearanceData;
        vehicle.BodyHealth = vehicleData.BodyHealth;
        vehicle.DashboardColor = vehicleData.DashboardColor;
        vehicle.Livery = vehicleData.Livery;
        vehicle.EngineHealth = vehicleData.EngineHealth;
        vehicle.CounterMeasureCount = vehicleData.CounterMeasureCount;
        vehicle.CustomTires = vehicleData.CustomTires;
        vehicle.DirtLevel = vehicleData.DirtLevel;
        vehicle.DriftMode = vehicleData.DriftMode;
        vehicle.HeadlightColor = vehicleData.HeadlightColor;
        vehicle.HybridExtraActive = vehicleData.HybridExtraActive;
        vehicle.HybridExtraState = vehicleData.HybridExtraState;
        vehicle.InteriorColor = vehicleData.InteriorColor;
        vehicle.IsRoofClosed = vehicleData.IsRoofClosed;
        vehicle.LightsMultiplier = vehicleData.LightsMultiplier;
        vehicle.LockState = (VehicleLockState)vehicleData.LockState;
        vehicle.ManualEngineControl = true;
        vehicle.NumberplateIndex = vehicleData.NumberplateIndex;
        vehicle.NumberplateText = vehicleData.NumberplateText;
        vehicle.PearlColor = vehicleData.PearlColor;
        vehicle.PetrolTankHealth = vehicleData.PetrolTankHealth;
        vehicle.PrimaryColorRgb = vehicleData.PrimaryColorRgb;
        vehicle.SecondaryColorRgb = vehicleData.SecondaryColorRgb;
        vehicle.RearWheel = vehicleData.RearWheel;
        vehicle.RoofLivery = vehicleData.RoofLivery;
        vehicle.ScriptMaxSpeed = vehicleData.ScriptMaxSpeed;
        vehicle.SpecialDarkness = vehicleData.SpecialDarkness;
        vehicle.WheelColor = vehicleData.WheelColor;
        vehicle.WindowTint = vehicleData.WindowTint;
        
        byte i = 0;
        foreach (short mod in vehicleData.Mods)
        {
            if (mod == -1) continue;
            vehicle.SetMod(i, (byte)mod);
            i++;
        }
    }

    public static void OnVehicleDamage(IVehicle target, uint bodyHealthDamage)
    {
        int damage = bodyHealthDamage switch
        {
            <= 25 => (int)bodyHealthDamage,
            <= 40 => (int)bodyHealthDamage * 4,
            >= 40 => (int)(bodyHealthDamage * 13.7)
        };

        if (target.EngineHealth - damage <= 0) target.EngineHealth = 0;
        else target.EngineHealth -= damage;
        if (damage >= 1200) target.EngineHealth = -300;
        if (target.EngineHealth <= 50) target.EngineOn = false;
    }
}