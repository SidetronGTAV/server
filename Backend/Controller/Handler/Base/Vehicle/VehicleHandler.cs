using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Models;
using Common.Models.Base;
using Common.Models.Entity.Vehicle;
using DataAccess.DbHandler;
using Position = Common.Models.Base.Position;

namespace Controller.Handler.Base.Vehicle;

public class VehicleHandler
{
    public static async Task<IVehicle?> CreateExistingVehicleAsync(int vehicleId)
    {
        var dbVehicle = await VehicleDbHandler.GetVehicleByIdAsync(vehicleId);
        if (dbVehicle is null) return null;

        var vehicle = (MyVehicle)await AltAsync.CreateVehicle(dbVehicle.Model, dbVehicle.Position, dbVehicle.Rotation);
        SetVehicleData(vehicle, dbVehicle);
        return vehicle;
    }

    public static void SetVehicleData(MyVehicle vehicle, Common.Models.Entity.Vehicle.Vehicle dbVehicle)
    {
        var vehicleData = dbVehicle.VehicleData;

        vehicle.VehicleId = dbVehicle.Id;
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

    public static void GetVehicleData(MyVehicle vehicle, Common.Models.Entity.Vehicle.Vehicle dbVehicle)
    {
        var vehicleData = dbVehicle.VehicleData;

        vehicleData.HealthData = vehicle.HealthData;
        vehicleData.DamageData = vehicle.DamageData;
        vehicleData.AppearanceData = vehicle.AppearanceData;
        vehicleData.BodyHealth = vehicle.BodyHealth;
        vehicleData.DashboardColor = vehicle.DashboardColor;
        vehicleData.Livery = vehicle.Livery;
        vehicleData.EngineHealth = vehicle.EngineHealth;
        vehicleData.CounterMeasureCount = vehicle.CounterMeasureCount;
        vehicleData.CustomTires = vehicle.CustomTires;
        vehicleData.DirtLevel = vehicle.DirtLevel;
        vehicleData.DriftMode = vehicle.DriftMode;
        vehicleData.HeadlightColor = vehicle.HeadlightColor;
        vehicleData.HybridExtraActive = vehicle.HybridExtraActive;
        vehicleData.HybridExtraState = vehicle.HybridExtraState;
        vehicleData.InteriorColor = vehicle.InteriorColor;
        vehicleData.IsRoofClosed = vehicle.IsRoofClosed;
        vehicleData.LightsMultiplier = vehicle.LightsMultiplier;
        vehicleData.LockState = (byte)vehicle.LockState;
        vehicleData.NumberplateIndex = vehicle.NumberplateIndex;
        vehicleData.NumberplateText = vehicle.NumberplateText;
        vehicleData.PearlColor = vehicle.PearlColor;
        vehicleData.PetrolTankHealth = vehicle.PetrolTankHealth;
        vehicleData.PrimaryColorRgb = new RGBA
        {
            R = vehicle.PrimaryColorRgb.R, G = vehicle.PrimaryColorRgb.G, B = vehicle.PrimaryColorRgb.B,
            A = vehicle.PrimaryColorRgb.A
        };
        vehicleData.SecondaryColorRgb = new RGBA
        {
            R = vehicle.SecondaryColorRgb.R, G = vehicle.SecondaryColorRgb.G, B = vehicle.SecondaryColorRgb.B,
            A = vehicle.SecondaryColorRgb.A
        };
        vehicleData.RearWheel = vehicle.RearWheel;
        vehicleData.RoofLivery = vehicle.RoofLivery;
        vehicleData.ScriptMaxSpeed = vehicle.ScriptMaxSpeed;
        vehicleData.SpecialDarkness = vehicle.SpecialDarkness;
        vehicleData.WheelColor = vehicle.WheelColor;
        vehicleData.WindowTint = vehicle.WindowTint;

        for (byte i = 0; i < 50; i++) vehicleData.Mods[i] = vehicle.GetMod(i);
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

    public static async Task CreateNewVehicleAsync(MyPlayer player, uint model, Position position)
    {
        var vehicle = (MyVehicle)await AltAsync.CreateVehicle(model, position, new Rotation());
        var dbVehicle = new Common.Models.Entity.Vehicle.Vehicle
        {
            Model = model,
            Position = position,
            Rotation = new Position(),
            Garage = null,
            IsInGarage = false,
            CharacterId = player.IsInCharacterId,
            VehicleData = new VehicleData()
        };
        GetVehicleData(vehicle, dbVehicle);
        var newVehicle = await VehicleDbHandler.CreateVehicleAsync(dbVehicle);
        vehicle.VehicleId = newVehicle.Id;
    }

    public static async Task SpawnAllUnparkedVehicleAsync()
    {
        var vehicles = await VehicleDbHandler.GetAllUnparkedVehicleAsync();
        foreach (var dbVehicle in vehicles)
        {
            var vehicle = (MyVehicle)await AltAsync.CreateVehicle(dbVehicle.Model, dbVehicle.Position, dbVehicle.Rotation);
            SetVehicleData(vehicle, dbVehicle);
        }
    }
}