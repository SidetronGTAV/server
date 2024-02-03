using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Factories;
using Controller.Handler.Base;
using Controller.Handler.Base.Vehicle;
using DataAccess.DbHandler;
using Microsoft.EntityFrameworkCore;
using DbContext = DataAccess.DbContext;

namespace Controller;

internal class Start : AsyncResource
{
    public override void OnStart()
    {
        using var db = new DbContext();
        if (db.Database.EnsureCreated()) db.Database.Migrate();
        OperatorOnStart();
    }

    public override void OnStop()
    {
    }

    public override IEntityFactory<IPlayer> GetPlayerFactory()
    {
        return new MyPlayerFactory();
    }

    public override IEntityFactory<IVehicle> GetVehicleFactory()
    {
        return new MyVehicleFactory();
    }

    private static void OperatorOnStart()
    {
        new PlayerShitSaver();
        new VehicleShitSaver();
        Task.Run(VehicleHandler.SpawnAllUnparkedVehicleAsync);
    }
}