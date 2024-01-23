using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;

namespace Common.Models;

public class MyVehicle : AsyncVehicle, IAsyncConvertible<MyVehicle>
{
    public MyVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
    }

    public new MyVehicle ToAsync(IAsyncContext _) => this;
}