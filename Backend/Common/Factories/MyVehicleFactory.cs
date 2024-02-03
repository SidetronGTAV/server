using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Models;

namespace Common.Factories;

public class MyVehicleFactory : IEntityFactory<IVehicle>
{
    public IVehicle Create(ICore core, IntPtr entityPointer, uint id)
    {
        return new MyVehicle(core, entityPointer, id);
    }
}