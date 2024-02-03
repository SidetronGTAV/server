using System.Collections.Concurrent;
using System.Timers;
using AltV.Net;
using Common.Models;
using DataAccess.DbHandler;
using Timer = System.Timers.Timer;

namespace Controller.Handler.Base;

public class VehicleShitSaver
{
    private const int VehiclesPerTick = 10;
    private const int TickInterval = 1000;

    private readonly ConcurrentQueue<MyVehicle?> _allFuckingVehicles = new();

    public VehicleShitSaver()
    {
        var t = new Timer(TickInterval);
        t.Elapsed += OnSaveShit;
        t.Start();
    }

    private void OnSaveShit(object? source, ElapsedEventArgs e)
    {
        if (_allFuckingVehicles.IsEmpty)
            foreach (var vehicle in Alt.GetAllVehicles())
                _allFuckingVehicles.Enqueue((MyVehicle)vehicle);

        for (var i = 0; i < VehiclesPerTick; i++)
        {
            if (!_allFuckingVehicles.TryDequeue(out var vehicle)) return;
            if (vehicle is { VehicleId: 0 }) continue;
            VehicleDbHandler.SaveVehiclePosition(vehicle);
        }
    }
}