using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using Common.Extensions;

namespace Common.Models;

public class MyVehicle : AsyncVehicle, IAsyncConvertible<MyVehicle>
{
    public float CurrentSpeed => this.GetSpeed();
    
    public MyVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
    }

    public new MyVehicle ToAsync(IAsyncContext _) => this;
}