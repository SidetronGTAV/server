using AltV.Net;
using AltV.Net.Elements.Entities;
using Controller.Handler.Base.Vehicle;

namespace Controller.Controller.Base.Vehicle;

public class VehicleController : IScript
{
    public VehicleController()
    {
        Alt.OnVehicleDamage += OnVehicleDamage;
    }

    private static void OnVehicleDamage(IVehicle target, IEntity attacker, uint bodyHealthDamage,
        uint additionalBodyHealthDamage, uint engineHealthDamage, uint petrolTankDamage, uint weaponHash)
    {
        VehicleHandler.OnVehicleDamage(target, bodyHealthDamage);
    }
}