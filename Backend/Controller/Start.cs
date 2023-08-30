using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Factories;

namespace Controller;

internal class Start: Resource
{
    public override void OnStart()
    {
        Console.WriteLine("Started");
    }

    public override void OnStop()
    {
        Console.WriteLine("Stopped");
    }
    
    public override IEntityFactory<IPlayer> GetPlayerFactory()
    {
        return new MyPlayerFactory();
    }
}