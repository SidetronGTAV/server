using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Factories;
using Controller.Controller.Base;
using Controller.Handler;
using Controller.Handler.Base;
using Microsoft.EntityFrameworkCore;
using DbContext = DataAccess.DbContext;

namespace Controller;

internal class Start : AsyncResource
{
    public override void OnStart()
    {
        using var db = new DbContext();
        if (db.Database.EnsureCreated())
        {
            db.Database.Migrate();
        }

        Console.WriteLine("Started");
        new ShitSaver();
    }

    public override void OnStop()
    {
        Console.WriteLine("Stopped");
    }

    public override IEntityFactory<IPlayer> GetPlayerFactory()
    {
        return new MyPlayerFactory();
    }
    
    public override IEntityFactory<IVehicle> GetVehicleFactory()
    {
        return new MyVehicleFactory();
    }
}